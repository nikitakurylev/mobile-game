using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedResourceTarget : DroppedTarget
{
    [SerializeField] private ResourceEnum _resource;
    private bool _isFree = true;

    public override ResourceEnum Resource => _resource;
    public override int Priority { get; } = 0;

    public override void Occupy(int amount)
    {
        if (!_isFree)
            throw new UnityException("Dropped already occupied");
        _isFree = false;
    }

    public override void PickUp(int amount)
    {
        if (_isFree)
            throw new UnityException("Dropped picked without occupying first");
        Destroy(gameObject);
    }

    public override int GetAvailableResources()
    {
        return _isFree ? 1 : 0;
    }
}
