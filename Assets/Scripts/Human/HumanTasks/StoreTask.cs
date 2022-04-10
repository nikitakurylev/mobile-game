using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            HumanController.InventoryResource = ResourceEnum.None;
            HumanController.InventoryCount -= _resourceCount;
            _storageTarget.Store(_resourceCount);
            FinishTask();
        }
    }
}