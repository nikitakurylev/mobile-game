using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSaveRepository : MonoBehaviour, ISaveRepository
{
    [SerializeField] private List<Stat> _stats;
    private string _file;
    private bool _hasInit = false;

    private void Awake()
    {
       Init();
    }

    private void Init()
    {
        if(_hasInit)
            return;;
        _file = Application.persistentDataPath + "/save.json";
        if (File.Exists(_file))
        {
            _stats = new List<Stat>();
            string[] data = File.ReadAllText(_file).Split(';');
            for (var i = 0; i < data.Length - 1; i++)
                _stats.Add(JsonUtility.FromJson<Stat>(data[i]));
        }
        else
        {
            File.Create(_file);
            _stats = new List<Stat>();
            Flush();
        }

        _hasInit = true;
    }

    private void Flush()
    {
        Init();
        string data = String.Empty;
        foreach (Stat stat in _stats)
        {
            data += JsonUtility.ToJson(stat) + ";";
        }
        File.WriteAllText(_file, data);
    }

    public void Save(string key, int value)
    {
        Init();
        Stat stat = _stats.Find(stat => stat.StatName == key);
        if (stat != null)
            stat.BaseValue = value;
        else
            _stats.Add(new Stat(key, value));
        Flush();
    }

    public int Load(string key)
    {
        Init();
        Stat stat = _stats.Find(stat => stat.StatName == key);
        if (stat != null)
            return stat.BaseValue;
        return 0;
    }

    public List<Stat> LoadAll()
    {
        Init();
        return _stats;
    }
}
