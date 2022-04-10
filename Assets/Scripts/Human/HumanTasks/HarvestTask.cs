using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarvestTask : HumanTask
{
    private ResourceTarget _resourceTarget;
    private bool _isMoving = false;
    
    public HarvestTask(ResourceTarget resourceTarget)
    {
        _resourceTarget = resourceTarget;
    }

    protected override void StartTask()
    {
        HumanController.MoveTo(_resourceTarget);
        _isMoving = true;
    }

    public override void OnActionFinish()
    {
        if (_isMoving)
        {
            _isMoving = false;
            HumanController.ExecuteAction("harvest");
        }
        else
            HumanController.FinishTask();
    }
}