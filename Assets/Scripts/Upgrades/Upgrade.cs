using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Upgrade : StorageIndicator
{
    [SerializeField] private UnityEvent _onBuildingFinish;
    [SerializeField] private UpgradeInfo _upgradeInfo;
    [SerializeField] private Transform _progressBar;
    [SerializeField] private Text _upgradeNameText;
    [SerializeField] private float _upgradeTime = 5f;
    private HashSet<Storage> _registeredStorages;
    private int _totalCapacity = 0;
    private bool _isUpdating = false;

    public UpgradeInfo UpgradeInfo => _upgradeInfo;

    private void Awake()
    {
        _registeredStorages = new HashSet<Storage>();
        _upgradeNameText.text = _upgradeInfo.Name;
    }

    private IEnumerator ProgressBar()
    {
        _progressBar.parent.gameObject.SetActive(true);
        float finishTime = Time.time + _upgradeTime;
        while (Time.time < finishTime)
        {
            _progressBar.transform.localScale = new Vector3(1 - (finishTime - Time.time) / _upgradeTime, 1, 1);
            yield return null;
        }
        FinishBuilding();
    }
    
    public override void UpdateIndicator(Storage storage)
    {
        if(_isUpdating)
            return;
        _isUpdating = true;
        if (!_registeredStorages.Contains(storage))
        {
            int amount = _upgradeInfo.NeededResources.Find(resource => resource.ResourceType == storage.ResourceType)
                .Amount;
            storage.StorageCapacity = amount;
            _totalCapacity += amount;
            _registeredStorages.Add(storage);
        }

        int itemCount = 0;
        foreach (Storage registeredStorage in _registeredStorages)
            itemCount += registeredStorage.ItemCount;
        if (itemCount >= _totalCapacity)
            StartCoroutine(ProgressBar());
        _isUpdating = false;
    }

    public void FinishBuilding()
    {
        foreach (StorageTarget storageTarget in GetComponents<StorageTarget>())
            storageTarget.enabled = false;
        FindObjectOfType<UpgradePanelManager>(true).FinishUpgrade(this);
        _onBuildingFinish.Invoke();
    }
}
