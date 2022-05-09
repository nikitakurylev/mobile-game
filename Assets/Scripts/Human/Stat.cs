using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private string _statName;
 
    [SerializeField] private int _baseValue;

    public string StatName => _statName;

    public int BaseValue
    {
        get => _baseValue;
        set => _baseValue = value;
    }

    public Stat(string statName, int baseValue)
    {
        _statName = statName;
        _baseValue = baseValue;
    }
}