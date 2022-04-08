using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedTarget : HumanTarget
{
    protected bool free = true;
    
    public override void Occupy()
    {
        free = false;
    }

    public override bool IsFree()
    {
        return free;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
