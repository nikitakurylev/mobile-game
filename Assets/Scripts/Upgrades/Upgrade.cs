using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : StorageIndicator
{
    [SerializeField] private UnityEvent _onBuildingFinish;
    [SerializeField] private UpgradeInfo _upgradeInfo;
    private HashSet<Storage> _registeredStorages;
    private int _totalCapacity = 0;

    public UpgradeInfo UpgradeInfo => _upgradeInfo;

    private void Awake()
    {
        _registeredStorages = new HashSet<Storage>();
    }
    
    public override void UpdateIndicator(Storage storage)
    {
        if (!_registeredStorages.Contains(storage))
        {
            _totalCapacity += storage.StorageCapacity;
            _registeredStorages.Add(storage);
        }

        int itemCount = 0;
        foreach (Storage registeredStorage in _registeredStorages)
            itemCount += registeredStorage.ItemCount;
        if(itemCount >= _totalCapacity)
            FinishBuilding();
    }

    public void FinishBuilding()
    {
        foreach (StorageTarget storageTarget in GetComponents<StorageTarget>())
            storageTarget.enabled = false;
        FindObjectOfType<UpgradePanelManager>(true).FinishUpgrade(this);
        _onBuildingFinish.Invoke();
    }
}
