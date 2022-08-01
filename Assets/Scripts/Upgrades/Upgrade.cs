using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Upgrade : StorageIndicator
{
    [SerializeField] private UnityEvent _onBuildingFinish;
    [SerializeField] private UnityEvent _onAwake;
    [SerializeField] private UpgradeInfo _upgradeInfo;
    [SerializeField] private float _upgradeTime = 5f;
    [SerializeField] private Vector3 _panelOffset;
    [SerializeField] private BuildTarget _buildTarget;
    private HashSet<Storage> _registeredStorages;
    private int _totalCapacity = 0;
    private bool _isUpdating = false;

    public UpgradeInfo UpgradeInfo => _upgradeInfo;

    private void Awake()
    {
        _registeredStorages = new HashSet<Storage>();
        BuildPanel.Instance.SetName(_upgradeInfo.DisplayName);
        BuildPanel.Instance.SetPos(transform.position, _panelOffset);
        _buildTarget.enabled = false;
        _totalCapacity = _upgradeInfo.NeededResources.Sum(resource => resource.Amount);
        HumanPlanner.CancelAll();
        SaveManager.SetData(_upgradeInfo.ID, 2);
        _onAwake.Invoke();
    }

    private IEnumerator ProgressBar()
    {
        BuildPanel.Instance.ShowProgressBar();
        float finishTime = Time.time + _upgradeTime - SaveManager.GetData("craft_speed") * 0.1f;
        while (Time.time < finishTime)
        {
            BuildPanel.Instance.SetProgress(1 - (finishTime - Time.time) / _upgradeTime);
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
            _registeredStorages.Add(storage);
        }
        BuildPanel.Instance.UpdateIndicator(storage);
        int itemCount = 0;
        foreach (Storage registeredStorage in _registeredStorages)
            itemCount += registeredStorage.ItemCount;
        if (itemCount >= _totalCapacity)
        {
            StartCoroutine(ProgressBar());
            _buildTarget.Active = true;
        }

        _isUpdating = false;
    }

    public void FinishBuilding()
    {
        _buildTarget.enabled = false;
        foreach (StorageTarget storageTarget in GetComponents<StorageTarget>())
            Destroy(storageTarget);
        foreach (Storage storage in GetComponents<Storage>())
        {
            storage.ItemCount = 0;
            Destroy(storage);
        }

        FindObjectOfType<UpgradePanelManager>(true).FinishUpgrade(this);
        _buildTarget.Active = false;
        Destroy(_buildTarget);
        BuildPanel.Instance.HideProgressBar();
        _onBuildingFinish.Invoke();
    }
}
