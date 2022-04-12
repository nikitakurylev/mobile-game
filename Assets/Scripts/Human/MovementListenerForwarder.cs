using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementListenerForwarder : MonoBehaviour, IMovementListener
{
    [SerializeField] private MonoBehaviour _listener;
    private IMovementListener _movementListener;

    private void OnValidate()
    {
        if (_listener == null)
            throw new UnityException("No Movement Listener assigned");
        GameObject listenerGameObject = _listener.gameObject;
        _listener = listenerGameObject.GetComponent<IMovementListener>() as MonoBehaviour;
        if (_listener == null)
            throw new UnityException("No IMovementListener attached to " + listenerGameObject.name);
    }

    public void OnArrive()
    {
        _movementListener.OnArrive();
    }

    public void OnActionFinished()
    {
        _movementListener.OnActionFinished();
    }
}
