using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroppedTarget : HumanTarget
{
    public abstract ResourceEnum Resource { get; }
    public abstract void Occupy(int amount);
    public abstract void PickUp(int amount);
    public abstract int GetAvailableResources();
}
