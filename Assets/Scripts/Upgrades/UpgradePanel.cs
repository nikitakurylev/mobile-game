using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<Sprite> _resourceIcons;
    [SerializeField] private List<Image> _resourceImages;
    [SerializeField] private List<Text> _resourceCounters;
    [SerializeField] private Text _upgradeName;
    [SerializeField] private Text _upgradeDescription;
    [SerializeField] private Text _requirements;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    private UpgradeInfo _upgradeInfo;
    private List<UpgradeInfo> _previousUpgrades;

    public UpgradeInfo UpgradeInfo => _upgradeInfo;

    private void UpdateRequirements()
    {
        _requirements.text = "To unlock, build these:\n";
        for (int i = 0; i < _previousUpgrades.Count - 1; i++)
        {
            _requirements.text += "\"" + _previousUpgrades[i].DisplayName + "\", ";
        }

        _requirements.text += "\"" + _previousUpgrades.Last().DisplayName + "\"";
    }
    
    public void SetUpgrade(UpgradeInfo upgradeInfo)
    {
        _upgradeName.text = upgradeInfo.DisplayName;
        _upgradeDescription.text = upgradeInfo.Description;
        for (int i = 0; i < upgradeInfo.NeededResources.Count; i++)
        {
            _resourceImages[i].sprite = _resourceIcons[(int) upgradeInfo.NeededResources[i].ResourceType];
            _resourceCounters[i].text = upgradeInfo.NeededResources[i].Amount.ToString();
            _icon.sprite = upgradeInfo.UpgradeIcon;
        }

        _upgradeInfo = upgradeInfo;
        _previousUpgrades = new List<UpgradeInfo>();
    }

    public void SetButtonActive(bool isActive)
    {
        _button.gameObject.SetActive(isActive);
    }

    public void SetButtonInteractable(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    public void AddButtonListener(UnityAction call)
    {
        _button.onClick.AddListener(call);
    }

    public void AddPreviousUpgrade(UpgradeInfo upgradeInfo)
    {
        _previousUpgrades.Add(upgradeInfo);
        UpdateRequirements();
    }

    public void FinishPreviousUpgrade(UpgradeInfo upgradeInfo)
    {
        _previousUpgrades.Remove(upgradeInfo);
        if (_previousUpgrades.Count == 0)
        {
            _requirements.text = "";
            SetButtonActive(true);
        }
        else
            UpdateRequirements();
    }
}