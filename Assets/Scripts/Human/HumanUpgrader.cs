using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanUpgrader : MonoBehaviour
{
    [SerializeField] private List<HumanUpgradePlan> _humanUpgradePlans;
    public void UpdateHuman(string statName)
    {
        _humanUpgradePlans.Find(plan => plan.StatName == statName)?.UpgradeEvents[HumanUpgradeManager.GetStat(statName)].Invoke();
    }
}
