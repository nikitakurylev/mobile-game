using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTarget : HumanTarget
{
    [SerializeField] private ResourceEnum _resource;
    public override void Occupy()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsFree()
    {
        throw new System.NotImplementedException();
    }
}
