using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedTarget : HumanTarget
{
    [SerializeField] private ResourceEnum _resource;

    public ResourceEnum Resource => _resource;

    protected bool free = true;
    
    public void Occupy()
    {
        if (!IsFree())
            throw new UnityException("Dropped already occupied");
        free = false;
    }

    public bool IsFree()
    {
        return free;
    }

    public void PickUp()
    {
        if (IsFree())
            throw new UnityException("Dropped picked without occupying first");
        Destroy(gameObject);
    }
}
