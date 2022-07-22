using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HumanUpgradePlan
{
    [SerializeField] private string _statName;
    [NonReorderable][SerializeField] private List<UnityEvent> _upgradeEvents;

    public string StatName => _statName;

    public List<UnityEvent> UpgradeEvents => _upgradeEvents;
}
