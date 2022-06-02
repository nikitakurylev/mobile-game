using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanUpgradePanelManager : StorageIndicator
{
    [SerializeField] private List<UpgradeInfo> _upgrades;
    [SerializeField] private GameObject _panelPrefab;
    [SerializeField] private Transform _panelParent;
    private List<UpgradePanel> _panels;
    private Storage _storage = null;

    private void Start()
    {
        _panels = new List<UpgradePanel>();
        foreach (UpgradeInfo upgradeInfo in _upgrades)
        {
            UpgradeInfo selectedInfo = upgradeInfo;
            for (int i = 0; i < SaveManager.GetData(upgradeInfo.ID); i++)
            {
                if (!selectedInfo.NextUpgrades.Any())
                    break;
                selectedInfo = selectedInfo.NextUpgrades[0];
            }

            UpgradePanel panel = Instantiate(_panelPrefab, _panelParent).GetComponent<UpgradePanel>();
            panel.SetUpgrade(selectedInfo);

            panel.SetButtonInteractable(false);
            panel.AddButtonListener((() => OnUpgrade(panel)));
            _panels.Add(panel);
        }
    }

    private void OnUpgrade(UpgradePanel panel)
    {
        if (_storage.ItemCount >= panel.UpgradeInfo.NeededResources[0].Amount)
        {
            _storage.ItemCount -= panel.UpgradeInfo.NeededResources[0].Amount;
            string id = panel.UpgradeInfo.ID;
            SaveManager.SetData(id, SaveManager.GetData(id) + 1);
            panel.SetUpgrade(panel.UpgradeInfo.NextUpgrades[0]);
        }
    }

    public override void UpdateIndicator(Storage storage)
    {
        if (_storage == null)
            _storage = storage;
        foreach (UpgradePanel upgradePanel in _panels)
        {
            upgradePanel.SetButtonInteractable(storage.ItemCount >= upgradePanel.UpgradeInfo.NeededResources[0].Amount);
        }
    }
}