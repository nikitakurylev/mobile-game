public abstract class HumanTask
{
    protected HumanController HumanController;

    public void ExecuteTask(HumanController humanController)
    {
        this.HumanController = humanController;
        StartTask();
    }
    
    protected abstract void StartTask();

    protected virtual void FinishTask()
    {
        HumanController.FinishTask();
    }
    
    public abstract void OnActionFinish();
}
