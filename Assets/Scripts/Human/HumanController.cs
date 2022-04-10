using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(IHumanMovement))]
public class HumanController : MonoBehaviour
{
    private Queue<HumanTask> _taskQueue;
    private IHumanMovement _movement;
    private Transform _movementTarget;
    private HumanPlanner _humanPlanner;
    [SerializeField] private Storage _storage;

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
    
    public int InventoryCapacity
    {
        get => _storage.StorageCapacity;
        set => _storage.StorageCapacity = value;
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
        _taskQueue = new Queue<HumanTask>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_movementTarget != null && other.transform == _movementTarget)
        {
            _movementTarget = null;
            _movement.Stop();
            _taskQueue.Peek().OnActionFinish();
        }
    }

    public void EnqueueTask(HumanTask humanTask)
    {
        _taskQueue.Enqueue(humanTask);
        if(_taskQueue.Count == 1)
            _taskQueue.Peek().ExecuteTask(this);
    }

    public void FinishTask()
    {
        _taskQueue.Dequeue();
        if(_taskQueue.Count == 0)
            _humanPlanner.OnHumanFinish(this); 
        _taskQueue.Peek().ExecuteTask(this);
    }

    public void MoveTo(HumanTarget humanTarget)
    {
        _movement.MoveTo(humanTarget.transform.position);
        _movementTarget = humanTarget.transform;
    }

    public void ExecuteAction(string actionName)
    {
        //dummy
        _taskQueue.Peek().OnActionFinish();
    }

    public bool HasFreeInventorySpace()
    {
        return _storage.HasFreeSpace();
    }
}