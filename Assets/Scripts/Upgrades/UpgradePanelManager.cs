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
    private List<Button> _panelButtons;

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
            if (SaveManager.GetData(upgrade.UpgradeInfo.name) == 1)
            {
                upgrade.gameObject.SetActive(true);
                upgrade.FinishBuilding();
            }
        }
    }

    private void Init()
    {
        _panelButtons = new List<Button>();
        
        foreach (Upgrade upgrade in _upgrades)
        {
            GameObject panel = Instantiate(_upgradePanelPrefab, _buttonParent);
            panel.GetComponent<UpgradePanel>().SetUpgrade(upgrade.UpgradeInfo);
            Button button = panel.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => upgrade.gameObject.SetActive(true));
            button.onClick.AddListener(() => _onUpgradeChosen.Invoke());
            button.gameObject.SetActive(false);
            _panelButtons.Add(button);
        }

        _panelButtons[0].gameObject.SetActive(true);
    }

    public void FinishUpgrade(Upgrade upgrade)
    {
        SaveManager.SetData(upgrade.UpgradeInfo.name, 1);
        _panelButtons[_upgrades.IndexOf(upgrade)].transform.parent.gameObject.SetActive(false);
        foreach (UpgradeInfo nextUpgradeInfo in upgrade.UpgradeInfo.NextUpgrades)
        {
            int index = _upgrades.FindIndex(nextUpgrade => nextUpgrade.UpgradeInfo == nextUpgradeInfo);
            if (index == -1)
                throw new UnityException("An Upgrade has not been added to PanelManager");
            _panelButtons[index].gameObject.SetActive(true); // TODO refactor
        }
        
        _onUpgradeFinish.Invoke();
    }
}
