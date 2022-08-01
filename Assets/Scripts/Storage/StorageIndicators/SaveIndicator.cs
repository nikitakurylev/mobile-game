using System.Collections.Generic;
using UnityEngine;

public class SaveIndicator : StorageIndicator
{
    [SerializeField] private string _id;
    private List<Storage> _savedStorages;
    private bool _isInitialized;
    private int _savedCount = -1;

    public List<Storage> SavedStorages => _savedStorages;

    public override void UpdateIndicator(Storage storage)
    {
        if (_savedStorages == null)
            _savedStorages = new List<Storage>();
        if(_isInitialized)
            SaveManager.SetData(_id, storage.ItemCount);
        else
        {
            if(_savedCount == -1)
                _savedCount = SaveManager.GetData(_id);
            if (storage.StorageCapacity >= _savedCount)
            {
                _savedStorages.Add(storage);
                storage.ItemCount = _savedCount;
                _isInitialized = true;
            }
        }
    }
}
