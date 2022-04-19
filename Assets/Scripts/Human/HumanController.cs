using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(HumanMovement), typeof(Storage))]
public class HumanController : MonoBehaviour, IActionListener
{
    [SerializeField] private Animator _animator;
    private Queue<HumanTask> _taskQueue;
    private HumanMovement _movement;
    private HumanPlanner _humanPlanner;
    private Storage _storage;

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
        //set => _storage.StorageCapacity = value;
    }

    private void OnValidate()
    {
        if (GetComponent<HumanMovement>() == null)
            throw new UnityException("No Human Movement");
        if (GetComponent<Storage>() == null)
            throw new UnityException("No Storage");
    }

    private void Awake()
    {
        _movement = GetComponent<HumanMovement>();
        _storage = GetComponent<Storage>();
        _humanPlanner = FindObjectOfType<HumanPlanner>();
        if (_humanPlanner == null)
            throw new UnityException("No Human Planner in scene");
        if (_animator == null)
            throw new UnityException("No Animator assigned");
        _taskQueue = new Queue<HumanTask>();
    }

    private void Start()
    {
        _movement.AddListener(this);
    }

    private void OnTriggerStay(Collider other)
    {
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
        _movement.MoveTo(humanTarget.transform);
    }

    public void ExecuteAction(string actionName)
    {
        _animator.SetTrigger(actionName);
    }

    public bool HasFreeInventorySpace()
    {
        return _storage.HasFreeSpace();
    }

    public void OnActionFinished()
    {
        _taskQueue.Peek().OnActionFinish();
    }
}