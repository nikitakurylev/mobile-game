public abstract class HumanTask
{
    private bool _taskFinished = false;
    
    protected HumanController HumanController;

    public void ExecuteTask(HumanController humanController)
    {
        if (_taskFinished)
        {
            humanController.FinishTask();
            return;
        }
        this.HumanController = humanController;
        StartTask();
    }
    
    protected abstract void StartTask();

    protected void FinishTask()
    {
        _taskFinished = true;
        if(HumanController != null)
            HumanController.FinishTask();
    }
    
    public abstract void OnActionFinish();

    public abstract void CancelTask();
}
