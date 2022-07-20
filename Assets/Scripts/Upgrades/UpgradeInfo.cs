using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Upgrade", order = 1)]
public class UpgradeInfo : ScriptableObject
{
    [SerializeField] private Sprite _upgradeIcon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _id;
    [SerializeField] private List<UpgradeInfo> _nextUpgrades;
    [SerializeField] private List<NeededResource> _neededResources;

    public Sprite UpgradeIcon => _upgradeIcon;

    public string DisplayName => _name;

    public string ID => _id;

    public List<UpgradeInfo> NextUpgrades => _nextUpgrades;

    public List<NeededResource> NeededResources => _neededResources;

    public string Description => _description;
}
