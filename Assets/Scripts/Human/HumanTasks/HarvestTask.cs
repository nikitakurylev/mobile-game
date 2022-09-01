using UnityEngine;

public class HarvestTask : HumanTask
{
    private ResourceTarget _resourceTarget;
    private int _resourceToHarvest;
    private bool _isMoving = false;
    private int _chopsLeft = 0;
    private bool _finished = false;

    public HarvestTask(ResourceTarget resourceTarget, int resourceCount)
    {
        if (resourceCount <= 0)
            throw new UnityException("Invalid Resource Count!");
        _resourceTarget = resourceTarget;
        _resourceTarget.Occupy(resourceCount);
        _resourceToHarvest = resourceCount;
    }

    protected override void StartTask()
    {
        _isMoving = true;
        //HumanController.InventoryResource = _resourceTarget.Resource;
        HumanController.MoveTo(_resourceTarget);
    }

    public override void OnActionFinish()
    {
        if (_isMoving)
        {
            _isMoving = false;
            HumanController.ExecuteAction("harvest_" + _resourceTarget.Resource);
            _chopsLeft = 6 - HumanStatManager.GetStat("harvest_" + _resourceTarget.Resource + "_speed");
        }
        else
        {
            if (!_finished)
            {
                _chopsLeft--;
                _resourceTarget.Chop();
            }

            if (_chopsLeft <= 0)
            {
                _resourceToHarvest--;
                _resourceTarget.Harvest(HumanController.transform.position);
                _chopsLeft = 7 - HumanStatManager.GetStat("harvest_" + _resourceTarget.Resource + "_speed");
            }
            if (_resourceToHarvest > 0)
                HumanController.ExecuteAction("harvest_" + _resourceTarget.Resource);
            if(_finished)
            {
                HumanController.FinishTask();
            }
            else if(_resourceToHarvest <= 0)
            {
                HumanController.ExecuteAction("idle");
                _finished = true;
            }
        }
    }

    public override void CancelTask()
    {
        if (_isMoving)
        {
            _resourceTarget.Vacant(_resourceToHarvest);
            FinishTask();
        }
        else
        {
            _resourceTarget.Vacant(_resourceToHarvest - 1);
            _resourceToHarvest = 1;
        }
    }
}