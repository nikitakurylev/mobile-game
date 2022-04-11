using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour, IHumanMovement
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _range = 1f;
    private bool _isMoving = false;
    private Transform _target;
    private List<IMovementListener> _listeners;

    private void Awake()
    {
        _listeners = new List<IMovementListener>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            var position = transform.position;
            Vector3 newPosition = position + (_target.position - position).normalized * _speed;
            transform.LookAt(newPosition);
            transform.position = newPosition;
            if ((transform.position - _target.position).sqrMagnitude <= _range * _range)
            {
                foreach (IMovementListener listener in _listeners)
                    listener.OnArrive();
                _isMoving = false;
            }
        }
    }

    public void AddListener(IMovementListener listener)
    {
        _listeners.Add(listener);
    }

    public void MoveTo(Transform targetTransform)
    {
        _target = targetTransform;
        _isMoving = true;
    }
}
