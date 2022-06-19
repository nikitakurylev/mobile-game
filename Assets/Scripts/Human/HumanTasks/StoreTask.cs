public class StoreTask : HumanTask
{
    private StorageTarget _storageTarget;
    private bool _isMoving = false;
    private int _resourceCount;

    public StoreTask(StorageTarget storageTarget, int resourceCount)
    {
        _storageTarget = storageTarget;
        _resourceCount = resourceCount;
        storageTarget.Occupy(resourceCount);
    }

    protected override void StartTask()
    {
        HumanController.MoveTo(_storageTarget);
        _isMoving = true;
    }

    public override void OnActionFinish()
    {
        if (_isMoving)
        {
            _isMoving = false;
            HumanController.ExecuteAction("store");
        }
        else
        {
            HumanController.InventoryCount -= _resourceCount;
            if(HumanController.InventoryCount <= 0)
                HumanController.InventoryResource = ResourceEnum.None;
            _storageTarget.Store(_resourceCount);
            FinishTask();
        }
    }

    public override void CancelTask()
    {
        if (_isMoving)
        {
            _storageTarget.Vacant(_resourceCount);
            FinishTask();
        }
    }
}