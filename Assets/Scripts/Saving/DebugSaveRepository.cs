using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSaveRepository : MonoBehaviour, ISaveRepository
{
    [SerializeField] private List<Stat> _data;
    
    public void Save(string key, int value)
    {
    }

    public int Load(string key)
    {
        return _data.Find(stat => stat.StatName == key).BaseValue;
    }

    public List<Stat> LoadAll()
    {
        return _data;
    }
}
