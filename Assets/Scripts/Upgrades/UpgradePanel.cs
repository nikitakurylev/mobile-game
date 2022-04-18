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

    public void SetUpgrade(Upgrade upgrade)
    {
        _upgradeName.text = upgrade.Name;
        for (int i = 0; i < upgrade.NeededResources.Count; i++)
        {
            _resourceImages[i].sprite = _resourceIcons[(int) upgrade.NeededResources[i].ResourceType];
            _resourceCounters[i].text = upgrade.NeededResources[i].Amount.ToString();
        }
    }
}