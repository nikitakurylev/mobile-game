using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpTask : HumanTask
{
    private DroppedTarget _droppedTarget;

    protected override void StartTask()
    {
        DroppedTarget[] droppedTargets = HumanController.FindTargets<DroppedTarget>();
        if (droppedTargets.Length == 0)
        {
            FinishTask();
            return;
        }
        _droppedTarget = droppedTargets
            .OrderBy(target => Vector3.SqrMagnitude(target.transform.position - HumanController.transform.position))
            .FirstOrDefault(target => target.IsFree());
        if (_droppedTarget == null)
            FinishTask();
        else
        {
            _droppedTarget.Occupy();
            HumanController.MoveTo(_droppedTarget);
        }
    }

    public override void OnArrive()
    {
        _droppedTarget.PickUp();
        FinishTask();
    }
}