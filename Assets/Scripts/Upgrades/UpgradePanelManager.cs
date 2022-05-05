using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private GameObject _upgradePanelPrefab;
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
        if(_panelButtons == null)
            Init();
    }

    private void Init()
    {
        _panelButtons = new List<Button>();
        
        foreach (Upgrade upgrade in _upgrades)
        {
            GameObject panel = Instantiate(_upgradePanelPrefab, transform);
            panel.GetComponent<UpgradePanel>().SetUpgrade(upgrade.UpgradeInfo);
            Button button = panel.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => upgrade.gameObject.SetActive(true));
            button.onClick.AddListener(() => _onUpgradeChosen.Invoke());
            button.interactable = false;
            _panelButtons.Add(button);
        }

        _panelButtons[0].interactable = true;
    }

    public void FinishUpgrade(Upgrade upgrade)
    {
        if(_panelButtons == null)
            Init();
        _panelButtons[_upgrades.IndexOf(upgrade)].interactable = false;
        foreach (UpgradeInfo nextUpgradeInfo in upgrade.UpgradeInfo.NextUpgrades)
        {
            _panelButtons[_upgrades.FindIndex(nextUpgrade => nextUpgrade.UpgradeInfo == nextUpgradeInfo)].interactable = true; // TODO refactor
        }
        
        _onUpgradeFinish.Invoke();
    }
}
