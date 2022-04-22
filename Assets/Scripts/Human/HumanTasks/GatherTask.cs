using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GatherTask : HumanTask
{
    private DroppedTarget _droppedTarget;
    private bool _isMoving = false;
    private int _amount;

    public GatherTask(DroppedTarget droppedTarget, int amount)
    {
        _droppedTarget = droppedTarget;
        _amount = amount;
        _droppedTarget.Occupy(amount);
    }

    protected override void StartTask()
    {
        HumanController.MoveTo(_droppedTarget);
        _isMoving = true;
    }

    public override void OnActionFinish()
    {
        if (_isMoving)
        {
            _isMoving = false;
            HumanController.ExecuteAction("gather");
        }
        else
        {
            HumanController.InventoryResource = _droppedTarget.Resource;
            HumanController.InventoryCount += _amount;
            _droppedTarget.PickUp(_amount);
            FinishTask();
        }
    }
}