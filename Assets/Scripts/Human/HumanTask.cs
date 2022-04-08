using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanTask
{
    protected HumanController HumanController;

    public void SetController(HumanController humanController)
    {
        this.HumanController = humanController;
        StartTask();
    }
    
    protected abstract void StartTask();

    protected void FinishTask()
    {
        HumanController.FinishTask();
    }
    
    public abstract void OnArrive();
}
