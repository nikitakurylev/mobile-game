using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    [SerializeField] private Sprite _upgradeIcon;
    [SerializeField] private string _name;
    [SerializeField] private Upgrade _neededUpgrade;
    [SerializeField] private List<NeededResource> _neededResources;

    public string Name => _name;

    public Upgrade NeededUpgrade => _neededUpgrade;

    public List<NeededResource> NeededResources => _neededResources;
}
