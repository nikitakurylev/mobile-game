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
    private List<IActionListener> _listeners;

    private void Awake()
    {
        _listeners = new List<IActionListener>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            var position = transform.position;
            Vector3 newPosition = position + (_target.position - position).normalized * _speed;
            newPosition = new Vector3(newPosition.x, 0, newPosition.z);
            transform.LookAt(newPosition);
            transform.position = newPosition;
            if ((transform.position - _target.position).sqrMagnitude <= _range * _range)
            {
                foreach (IActionListener listener in _listeners)
                    listener.OnActionFinished();
                _isMoving = false;
            }
        }
    }

    public void AddListener(IActionListener listener)
    {
        _listeners.Add(listener);
    }

    public void MoveTo(Transform targetTransform)
    {
        _target = targetTransform;
        _isMoving = true;
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
