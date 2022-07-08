using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float _jumpChance, _maxMoveDistance, _minTime, _maxTime;
    [SerializeField] private Animator _animator;
    private Vector3 _startPos;
    private static readonly int Jump1 = Animator.StringToHash("jump");

    private void Start()
    {
        _startPos = transform.position;
        Choice();
    }

    private void Choice()
    {
        string methodName;
        if (Random.Range(0f, 1f) <= _jumpChance)
            methodName = nameof(Jump);
        else
            methodName = nameof(RandomMove);
        Invoke(methodName, Random.Range(_minTime, _maxTime));
    }

    private void Jump()
    {
        StopAllCoroutines();
        if(_animator)
            _animator.SetTrigger(Jump1);
        Choice();
    }

    private void RandomMove()
    {
        StopAllCoroutines();
        Vector3 position = new Vector3(Random.Range(-_maxMoveDistance, _maxMoveDistance), 0,
            Random.Range(-_maxMoveDistance, _maxMoveDistance));
        StartCoroutine(MoveSmoothly(_startPos + position));
        Choice();
    }

    IEnumerator MoveSmoothly(Vector3 position)
    {
        Quaternion rotation = Quaternion.LookRotation(position - transform.position); 
        while ((position - transform.position).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5*Time.deltaTime);
            yield return null;
        }

        transform.position = position;
        transform.rotation = rotation;
    }
}
