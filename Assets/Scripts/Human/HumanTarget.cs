using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanTarget : MonoBehaviour
{
    public abstract void Occupy(HumanController humanController);
    public abstract bool IsFree();
}
