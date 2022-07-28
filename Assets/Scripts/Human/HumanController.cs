using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(HumanMovement), typeof(Storage))]
public class HumanController : MonoBehaviour, IActionListener
{
    [SerializeField] private Animator _animator;
    private Queue<HumanTask> _taskQueue;
    private HumanMovement _movement;
    private HumanPlanner _humanPlanner;
    private Storage _storage;
    private Dictionary<ResourceEnum, Storage> _storages;

    public ResourceEnum InventoryResource
    {
        get => _storage.ResourceType;
        set => _storage = _storages[value];
    }

    public int InventoryCount
    {
        get => _storage.ItemCount;
        set => _storage.ItemCount = value;
    }
    
    public int InventoryCapacity(ResourceEnum resourceType)
    {
        return _storages[resourceType].StorageCapacity;
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
        _humanPlanner = HumanPlanner.Instance;
        if (_humanPlanner == null)
            throw new UnityException("No Human Planner in scene");
        if (_animator == null)
            throw new UnityException("No Animator assigned");
        _taskQueue = new Queue<HumanTask>();
        _storages = new Dictionary<ResourceEnum, Storage>();
        foreach (Storage storage in GetComponents<Storage>())
        {
            _storages.Add(storage.ResourceType, storage);
        }
        InventoryResource = ResourceEnum.None;
    }

    private void Start()
    {
        _movement.AddListener(this);
        EnqueueTask(new IdleTask());
    }

    public void EnqueueTask(HumanTask humanTask)
    {
        _taskQueue.Enqueue(humanTask);
        if(_taskQueue.Count == 1)
            _taskQueue.Peek().ExecuteTask(this);
    }

    public void FinishTask()
    {
        _movement.Stop();
        _taskQueue.Dequeue();
        if(_taskQueue.Count == 0)
            _humanPlanner.OnHumanFinish(this); 
        _taskQueue.Peek().ExecuteTask(this);
    }

    public void MoveTo(HumanTarget humanTarget)
    {
        _movement.MoveTo(humanTarget.TargetTransform(this));
    }

    public void ExecuteAction(string actionName)
    {
        string floatName = actionName + "_speed";
        _animator.SetFloat(floatName,  1f + SaveManager.GetData(floatName) * 0.2f);
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

    public void CancelAll()
    {
        _taskQueue.Enqueue(_taskQueue.Dequeue());
        while (_taskQueue.Count > 1)
        {
            _taskQueue.Dequeue();
        }
        _taskQueue.Peek().CancelTask();
    }
}