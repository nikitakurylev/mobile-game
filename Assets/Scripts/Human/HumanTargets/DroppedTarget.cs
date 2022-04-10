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
        free = false;
    }

    public bool IsFree()
    {
        return free;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
