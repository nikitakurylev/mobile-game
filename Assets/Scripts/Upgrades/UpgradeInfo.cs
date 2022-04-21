using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Upgrade", order = 1)]
public class UpgradeInfo : ScriptableObject
{
    [SerializeField] private Sprite _upgradeIcon;
    [SerializeField] private string _name;
    [SerializeField] private List<UpgradeInfo> _nextUpgrades;
    [SerializeField] private List<NeededResource> _neededResources;

    public string Name => _name;

    public List<UpgradeInfo> NextUpgrades => _nextUpgrades;

    public List<NeededResource> NeededResources => _neededResources;
}
