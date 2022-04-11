using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanMovement
{
    void AddListener(IMovementListener listener);
    void MoveTo(Transform targetTransform);
}
