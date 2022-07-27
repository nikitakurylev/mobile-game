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
        _buildTarget.OnBuildFinish.AddListener(OnBuildEnd);
        HumanController.MoveTo(_buildTarget);
    }

    private void OnBuildEnd()
    {
        _finished = true;
        HumanController.ExecuteAction("idle");
    }

    public override void OnActionFinish()
    {
        if(_finished) {
            FinishTask();
            return;
        }
        _finished = true;
        if(_buildTarget != null)
            HumanController.transform.LookAt(_buildTarget.transform);
        HumanController.ExecuteAction("build");
    }

    public override void CancelTask()
    {
        OnBuildEnd();
    }
}
