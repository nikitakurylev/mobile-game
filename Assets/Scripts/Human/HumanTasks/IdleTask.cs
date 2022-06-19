public class IdleTask : HumanTask
{
    protected override void StartTask()
    {
        HumanController.ExecuteAction("idle");
    }

    public override void OnActionFinish()
    {
        FinishTask();
    }

    public override void CancelTask()
    {
    }
}