using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour, IHumanMovement
{
    [SerializeField] private float _speed = 1f;
    private bool _isMoving = false;
    private Vector3 _direction;
    
    private void FixedUpdate()
    {
        if(_isMoving)
            transform.Translate(_direction * _speed);
    }

    public void MoveTo(Vector3 position)
    {
        _direction = (position - transform.position).normalized;
        _isMoving = true;
    }

    public void Stop()
    {
        _isMoving = false;
    }
}
