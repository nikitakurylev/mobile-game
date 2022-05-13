using UnityEngine;

[RequireComponent(typeof(Storage))]
public class DroppedStorageTarget : DroppedTarget
{
    private Storage _storage;
    private int _occupied;

    public override ResourceEnum Resource => _storage.ResourceType;
    public override int Priority { get; } = 1;


    private void OnValidate()
    {
        if (GetComponent<Storage>() == null)
            throw new UnityException("No Storage");
    }

    private void Awake()
    {
        _storage = GetComponent<Storage>();
    }

    public override void Occupy(int amount)
    {
        if (amount > GetAvailableResources())
            throw new UnityException("Trying to occupy more than available");
        _occupied += amount;
    }

    public override void PickUp(int amount)
    {
        if (amount > _occupied)
            throw new UnityException("Trying to take more than occupied");
        _occupied -= amount;
        _storage.ItemCount -= amount;
    }

    public override int GetAvailableResources()
    {
        return _storage.ItemCount - _occupied;
    }
}