using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<Upgrade> _neededUpgrades;
    [SerializeField] private List<NeededResource> _neededResources;
}
