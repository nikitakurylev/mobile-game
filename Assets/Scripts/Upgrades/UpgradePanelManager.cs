using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private GameObject _upgradePanelPrefab;
    [SerializeField] private Transform _buttonParent;
    [SerializeField] private UnityEvent _onUpgradeChosen;
    [SerializeField] private UnityEvent _onUpgradeFinish;
    private List<UpgradePanel> _panels;

    private void OnValidate()
    {
        if (_upgradePanelPrefab.GetComponent<UpgradePanel>() == null)
            throw new UnityException("Assigned Prefab without Upgrade Panel");
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        foreach (Upgrade upgrade in _upgrades)
        {
            if (SaveManager.GetData(upgrade.UpgradeInfo.ID) == 1)
            {
                upgrade.gameObject.SetActive(true);
                upgrade.FinishBuilding();
            }
        }
    }

    private void Init()
    {
        _panels = new List<UpgradePanel>();
        
        foreach (Upgrade upgrade in _upgrades)
        {
            UpgradePanel panel = Instantiate(_upgradePanelPrefab, _buttonParent).GetComponent<UpgradePanel>();
            panel.SetUpgrade(upgrade.UpgradeInfo);
            panel.AddButtonListener(() => upgrade.gameObject.SetActive(true));
            panel.AddButtonListener(() => _onUpgradeChosen.Invoke());
            panel.SetButtonActive(false);
            _panels.Add(panel);
        }

        foreach (UpgradePanel upgradePanel in _panels)
        {
            foreach (UpgradeInfo nextUpgradeInfo in upgradePanel.UpgradeInfo.NextUpgrades)
            {
                int index = _upgrades.FindIndex(nextUpgrade => nextUpgrade.UpgradeInfo == nextUpgradeInfo);
                if (index == -1)
                    throw new UnityException("Upgrade \"" + nextUpgradeInfo.DisplayName + "\" has not been added to PanelManager");
                _panels[index].AddPreviousUpgrade(upgradePanel.UpgradeInfo);
            }
        }

        _panels[0].SetButtonActive(true);
    }

    public void FinishUpgrade(Upgrade upgrade)
    {
        SaveManager.SetData(upgrade.UpgradeInfo.ID, 1);
        _panels[_upgrades.IndexOf(upgrade)].gameObject.SetActive(false);
        foreach (UpgradeInfo nextUpgradeInfo in upgrade.UpgradeInfo.NextUpgrades)
        {
            int index = _upgrades.FindIndex(nextUpgrade => nextUpgrade.UpgradeInfo == nextUpgradeInfo);
            if (index == -1)
                throw new UnityException("Upgrade \"" + nextUpgradeInfo.DisplayName + "\" has not been added to PanelManager");
            _panels[index].FinishPreviousUpgrade(upgrade.UpgradeInfo);
        }
        
        _onUpgradeFinish.Invoke();
    }
}
