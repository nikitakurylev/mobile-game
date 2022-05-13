using UnityEngine;

public class StorageTarget : HumanTarget
{
    [SerializeField] private Storage _storage;
    [SerializeField] private int _priority = 0;
    private int _occupied = 0;

    public int Priority => _priority;
    public ResourceEnum Resource => _storage.ResourceType;
    
    private void Awake()
    {
        if (_storage == null)
            throw new UnityException("No Storage assigned");
    }

    public void Occupy(int count)
    {
        if (count > GetFreeSpace())
            throw new UnityException("Trying to occupy more than available");
        _occupied += count;
    }

    public void Store(int count)
    {
        _occupied -= count;
        if (_occupied < 0)
            throw new UnityException("Stored more than occupied");
        _storage.ItemCount += count;
    }

    public int GetFreeSpace()
    {
        return _storage.StorageCapacity - _storage.ItemCount - _occupied;
    }
}