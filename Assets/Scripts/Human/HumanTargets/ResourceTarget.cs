using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTarget : HumanTarget
{
    [SerializeField] private ResourceEnum _resource;
    
    public ResourceEnum Resource => _resource;

    public void Occupy(HumanController humanController)
    {
        throw new System.NotImplementedException();
    }

    public bool IsFree()
    {
        throw new System.NotImplementedException();
    }
}
