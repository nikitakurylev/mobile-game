using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HarvestTask : HumanTask
{
    private ResourceTarget _resourceTarget;
    private int _resourceToHarvest;
    
    public HarvestTask(ResourceTarget resourceTarget, int resourceCount)
    {
        _resourceTarget = resourceTarget;
        _resourceTarget.Occupy(resourceCount);
        _resourceToHarvest = resourceCount;
    }

    protected override void StartTask()
    {
        HumanController.MoveTo(_resourceTarget);
    }

    public override void OnActionFinish()
    {
        if (_resourceToHarvest > 0)
        {
            _resourceToHarvest--;
            _resourceTarget.Harvest();
            HumanController.ExecuteAction("harvest");
        }
        else
            HumanController.FinishTask();
    }
}