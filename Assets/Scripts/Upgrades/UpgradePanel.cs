using System.Collections.Generic;
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
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    private UpgradeInfo _upgradeInfo;

    public UpgradeInfo UpgradeInfo => _upgradeInfo;

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
}