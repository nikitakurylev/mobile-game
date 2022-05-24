using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageFiller : StorageIndicator
{
    [SerializeField] private float _refillTime = 45f;
    [SerializeField] private Animator _animator;
    private HashSet<Storage> _storages;
    private static readonly int Harvest1 = Animator.StringToHash("harvest");

    private void Awake()
    {
        _storages = new HashSet<Storage>();
    }

    private IEnumerator Refill(Storage storage, float delay)
    {
        yield return new WaitForSeconds(delay);
        storage.ItemCount = storage.StorageCapacity;
        if (_animator != null)
        {
            _animator.SetTrigger(Harvest1);
        }

        _storages.Remove(storage);
    }
    
    public override void UpdateIndicator(Storage storage)
    {
        if(!storage.HasFreeSpace())
            return;
        if (_storages.Contains(storage))
            return;
        _storages.Add(storage);
        StartCoroutine(Refill(storage, _refillTime));
    }
}
