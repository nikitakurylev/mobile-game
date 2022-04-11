using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarvestTask : HumanTask
{
    private ResourceTarget _resourceTarget;
    private int _resourceToHarvest;
    private bool _isMoving = false;
    
    public HarvestTask(ResourceTarget resourceTarget, int resourceCount)
    {
        _resourceTarget = resourceTarget;
        _resourceTarget.Occupy(resourceCount);
        _resourceToHarvest = resourceCount;
    }

    protected override void StartTask()
    {
        _isMoving = true;
        HumanController.MoveTo(_resourceTarget);
    }

    public override void OnActionFinish()
    {
        if (_resourceToHarvest > 0)
        {
            if (_isMoving)
                _isMoving = false;
            else
            {
                _resourceToHarvest--;
                _resourceTarget.Harvest(); 
            }
            HumanController.ExecuteAction("harvest");
        }
        else
            HumanController.FinishTask();
    }
}