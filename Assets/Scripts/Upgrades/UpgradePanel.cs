using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<Sprite> _resourceIcons;
    [SerializeField] private List<Image> _resourceImages;
    [SerializeField] private List<Text> _resourceCounters;
    [SerializeField] private Text _upgradeName;

    public void SetUpgrade(UpgradeInfo upgradeInfo)
    {
        _upgradeName.text = upgradeInfo.Name;
        for (int i = 0; i < upgradeInfo.NeededResources.Count; i++)
        {
            _resourceImages[i].sprite = _resourceIcons[(int) upgradeInfo.NeededResources[i].ResourceType];
            _resourceCounters[i].text = upgradeInfo.NeededResources[i].Amount.ToString();
        }
    }
}