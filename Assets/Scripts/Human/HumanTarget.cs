using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanTarget : MonoBehaviour
{
    public abstract void Occupy();
    public abstract bool IsFree();
}
