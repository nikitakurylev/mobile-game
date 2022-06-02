using System.Collections.Generic;
using UnityEngine;

public class HumanStatManager : MonoBehaviour
{
    [SerializeField] Stat[] _baseStats;
    private Dictionary<string, int> _stats;
    private HumanUpgrader[] _humanUpgraders;
    private static HumanStatManager instance = null;
    private bool _isInitialized;

    public static HumanStatManager Instance => instance;

    HumanStatManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Awake(){
        _stats = new Dictionary<string, int>();
        foreach (Stat stat in _baseStats)
        {
            _stats.Add(stat.StatName, stat.BaseValue);
        }

        _humanUpgraders = FindObjectsOfType<HumanUpgrader>(true);
    }

    private static void UpdateHumans(string statName)
    {
        foreach (HumanUpgrader humanUpgrader in Instance._humanUpgraders)
        {
            humanUpgrader.UpdateHuman(statName);
        }
    }

    public void IncrementStat(string statName)
    {
        Instance._stats[statName]++;
        UpdateHumans(statName);
    }

    public static int GetStat(string statName)
    {
        return Instance._stats[statName];
    }
}
