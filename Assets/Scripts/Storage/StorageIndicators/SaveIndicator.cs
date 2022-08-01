using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveIndicator : StorageIndicator
{
    [SerializeField] private string _id;
    private bool _isInitialized;
    private int _savedCount = -1;
    public override void UpdateIndicator(Storage storage)
    {
        if(_isInitialized)
            SaveManager.SetData(_id, storage.ItemCount);
        else
        {
            if(_savedCount == -1)
                _savedCount = SaveManager.GetData(_id);
            if (storage.StorageCapacity >= _savedCount)
            {
                storage.ItemCount = _savedCount;
                _isInitialized = true;
            }
        }
    }
}
