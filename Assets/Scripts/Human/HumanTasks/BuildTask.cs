public class BuildTask : HumanTask
{
    private BuildTarget _buildTarget;
    private bool _finished = false;
    public BuildTask(BuildTarget target)
    {
        _buildTarget = target;
    }
    
    protected override void StartTask()
    {
        HumanController.MoveTo(_buildTarget);
    }

    public override void OnActionFinish()
    {
        if(_finished)
            return;
        _finished = true;
        if(_buildTarget != null)
            HumanController.transform.LookAt(_buildTarget.transform);
        HumanController.ExecuteAction("build");
        HumanController.FinishTask();
    }
}
