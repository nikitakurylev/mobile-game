using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleTask : HumanTask
{
    protected override void StartTask()
    {
        HumanController.StartCoroutine(WaitAndFinish());
    }

    IEnumerator WaitAndFinish()
    {
        yield return null;
        FinishTask();
    }
    
    public override void OnActionFinish()
    {
    }
}