using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanMovement
{
    void AddListener(IActionListener listener);
    void MoveTo(Transform targetTransform);
    float GetSpeed();
}
