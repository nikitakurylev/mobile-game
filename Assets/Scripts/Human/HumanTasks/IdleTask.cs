using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleTask : HumanTask
{
    protected override void StartTask()
    {
        HumanController.ExecuteAction("idle");
    }

    public override void OnActionFinish()
    {
        FinishTask();
    }
}