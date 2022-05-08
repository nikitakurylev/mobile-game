using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ISaveRepository))]
public class SaveManager : MonoBehaviour
{
    private static SaveManager instance = null;
    private ISaveRepository _saveRepository;
    private Dictionary<string, int> _saveCache;

    public static SaveManager Instance => instance;
    private void OnValidate()
    {
        if (GetComponent<ISaveRepository>() == null)
            throw new UnityException("No Save Repository");
    }
    
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

        _saveRepository = GetComponent<ISaveRepository>();
        _saveCache = new Dictionary<string, int>();
        foreach (Stat entry in _saveRepository.LoadAll())
            _saveCache.Add(entry.StatName, entry.BaseValue);

    }

    public static void SetData(string key, int value)
    {
        Instance._saveRepository.Save(key, value);
        Instance._saveCache[key] = value;
    }

    public static int GetData(string key)
    {
        return Instance._saveCache[key];
    }
}
