using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanMovement
{
    void MoveTo(Vector3 position);
    void Stop();
}
