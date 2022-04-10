using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Storage))]
public class StorageTarget : HumanTarget
{
    private Storage _storage;
    
    private void OnValidate()
    {
        if (GetComponent<Storage>() == null)
            throw new UnityException("No Storage");
    }

    
    public override void Occupy(HumanController humanController)
    {
    }

    public override bool IsFree()
    {
        return false;
    }
}