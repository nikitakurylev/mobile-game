using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagedMover : MonoBehaviour, IMover
{
    [SerializeField] private Transform[] _stages;
    [SerializeField] private float _speed = 5f;
    private int _currentStage = 0;

    IEnumerator MoveSmoothly(Transform target)
    {
        while ((target.position - transform.position).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, _speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public void Move()
    {
        StopAllCoroutines();
        StartCoroutine(MoveSmoothly(_stages[_currentStage]));
        _currentStage = (_currentStage + 1) % _stages.Length;
    }
}
