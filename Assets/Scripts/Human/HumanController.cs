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
    [SerializeField] private Storage _storage;

    public HumanTask HumanTask
    {
        get => _humanTask;
        set
        {
            _humanTask = value;
            _humanTask.SetController(this);
        }
    }

    public ResourceEnum InventoryResource
    {
        get => _storage.ResourceType;
        set => _storage.ResourceType = value;
    }

    public int InventoryCount
    {
        get => _storage.ItemCount;
        set => _storage.ItemCount = value;
    }

    private void OnValidate()
    {
        if (GetComponent<IHumanMovement>() == null)
            throw new UnityException("No Human Movement");
    }

    private void Awake()
    {
        _movement = GetComponent<IHumanMovement>();
        _humanPlanner = FindObjectOfType<HumanPlanner>();
        if (_humanPlanner == null)
            throw new UnityException("No Human Planner in scene");
        if (_storage == null)
            throw new UnityException("No Storage assigned");
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

    public void FinishTask()
    {
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

    public bool HasFreeInventorySpace()
    {
        return _storage.HasFreeSpace();
    }
}