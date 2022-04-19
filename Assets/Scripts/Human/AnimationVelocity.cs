using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HumanMovement))]
public class AnimationVelocity : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Vector3 _lastPosition;
    private float _movementSpeed = 1f;
    private static readonly int Velocity = Animator.StringToHash("velocity");

    private void OnValidate()
    {
        if (GetComponent<HumanMovement>() == null)
            throw new UnityException("No Human Movement");
    }

    private void Awake()
    {
        if(_animator == null)
            throw new UnityException("No Animator");
        _lastPosition = transform.position;
        _movementSpeed =  GetComponent<HumanMovement>().GetSpeed();
        _movementSpeed *= _movementSpeed;
    }

    void FixedUpdate()
    {
        _animator.SetFloat(Velocity, (transform.position - _lastPosition).sqrMagnitude / _movementSpeed );
        _lastPosition = transform.position;
    }
}
