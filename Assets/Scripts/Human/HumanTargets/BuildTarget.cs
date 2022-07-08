using UnityEngine.Events;

public class BuildTarget : HumanTarget
{
    private UnityEvent _onBuildFinish;
    private bool _active = false;

    public bool Active
    {
        get => _active;
        set
        {
            if (_active != value && value == false)
            {
                _active = false;
                _onBuildFinish.Invoke();
            }
            else
                _active = value;
        }
    }

    public BuildTarget()
    {
        _onBuildFinish = new UnityEvent();
    }

    public UnityEvent OnBuildFinish => _onBuildFinish;
}