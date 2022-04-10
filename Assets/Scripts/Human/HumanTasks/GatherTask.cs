using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GatherTask : HumanTask
{
    private DroppedTarget _droppedTarget;

    public GatherTask(DroppedTarget droppedTarget)
    {
        _droppedTarget = droppedTarget;
    }

    private void ChooseDroppedTarget()
    {
        if (_droppedTarget == null || (HumanController.InventoryResource != ResourceEnum.None && _droppedTarget.Resource != HumanController.InventoryResource))
            FinishTask();
        else
        {
            _droppedTarget.Occupy(HumanController);
            HumanController.MoveTo(_droppedTarget);
        }
    }

    protected override void StartTask()
    {
        ChooseDroppedTarget();
    }

    public override void OnArrive()
    {
        HumanController.InventoryResource = _droppedTarget.Resource;
        HumanController.InventoryCount++;
        _droppedTarget.PickUp();
        if (HumanController.HasFreeInventorySpace())
            ChooseDroppedTarget();
        else
            FinishTask();
    }

    protected override void FinishTask()
    {
        HumanController.InventoryResource = ResourceEnum.None;
        base.FinishTask();
    }
}