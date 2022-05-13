using System.Collections.Generic;
using UnityEngine;

public class HumanUpgradeManager : MonoBehaviour
{
    [SerializeField] Stat[] _baseStats;
    private Dictionary<string, int> _stats;
    private static HumanUpgradeManager instance = null;

    public static HumanUpgradeManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        _stats = new Dictionary<string, int>();
        foreach (Stat stat in _baseStats)
        {
            _stats.Add(stat.StatName, stat.BaseValue);
        }
    }

    public void IncrementStat(string statName)
    {
        Instance._stats[statName]++;
    }

    public static int GetStat(string statName)
    {
        return Instance._stats[statName];
    }
}
