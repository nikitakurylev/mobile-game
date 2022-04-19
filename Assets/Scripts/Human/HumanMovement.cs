using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanMovement : MonoBehaviour
{
    private List<IActionListener> _listeners;
    protected void Awake()
    {
        _listeners = new List<IActionListener>();
    }

    protected void InvokeListeners()
    {
        foreach (IActionListener listener in _listeners)
            listener.OnActionFinished();
    }
    public void AddListener(IActionListener listener)
    {
        _listeners.Add(listener);
    }
    public abstract void MoveTo(Transform targetTransform);
    public abstract float GetSpeed();
}
