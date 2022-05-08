using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private string _statName;
 
    [SerializeField] private int _baseValue;

    public string StatName => _statName;

    public int BaseValue => _baseValue;

}