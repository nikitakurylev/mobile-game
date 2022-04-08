using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IHumanMovement))]
public class HumanController : MonoBehaviour
{
    private HumanTask _humanTask;
    private IHumanMovement _movement;
    private Transform _movementTarget;
    private HumanPlanner _humanPlanner;
    
    private void OnValidate()
    {
        if (GetComponent<IHumanMovement>() == null)
            throw new UnityException("No Human Movement");
    }

    private void Awake()
    {
        _movement = GetComponent<IHumanMovement>();
        _humanPlanner = FindObjectOfType<HumanPlanner>();
        if(_humanPlanner == null)
            throw new UnityException("No Human Planner in scene");
    }

    public void SetTask(HumanTask humanTask)
    {
        _humanTask = humanTask;
        _humanTask.SetController(this);
    }

    public void FinishTask()
    {
        _humanTask = null;
        _humanPlanner.OnHumanFinish(this);
    }
    
    public T[] FindTargets<T>() where T : HumanTarget
    {
        return FindObjectsOfType<T>();
    }

    public void MoveTo(HumanTarget humanTarget)
    {
        _movement.MoveTo(humanTarget.transform.position);
        _movementTarget = humanTarget.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_movementTarget != null && other.transform == _movementTarget)
        {
            _movementTarget = null;
            _movement.Stop();
            _humanTask.OnArrive();
        }
    }
}
