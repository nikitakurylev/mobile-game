using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gather : HumanTask
{
    private DroppedTarget _droppedTarget;

    private DroppedTarget FindClosestFreeDroppedTarget()
    {
        DroppedTarget[] droppedTargets = HumanController.FindTargets<DroppedTarget>();
        if (droppedTargets.Length == 0)
        {
            return null;
        }

        return droppedTargets
            .OrderBy(target => Vector3.SqrMagnitude(target.transform.position - HumanController.transform.position))
            .FirstOrDefault(target => target.IsFree() && (HumanController.InventoryResource == ResourceEnum.None ||
                                                          target.Resource == HumanController.InventoryResource));
    }

    private void ChooseTarget()
    {
        _droppedTarget = FindClosestFreeDroppedTarget();

        if (_droppedTarget == null)
            FinishTask();
        else
        {
            _droppedTarget.Occupy(HumanController);
            HumanController.MoveTo(_droppedTarget);
        }
    }

    protected override void StartTask()
    {
        ChooseTarget();
    }

    public override void OnArrive()
    {
        HumanController.InventoryResource = _droppedTarget.Resource;
        HumanController.InventoryCount++;
        _droppedTarget.PickUp();
        if (HumanController.HasFreeInventorySpace())
            ChooseTarget();
        else
            FinishTask();
    }

    protected override void FinishTask()
    {
        HumanController.InventoryResource = ResourceEnum.None;
        base.FinishTask();
    }
}