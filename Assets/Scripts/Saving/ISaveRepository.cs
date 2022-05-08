using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveRepository
{
    void Save(string key, int value);
    int Load(string key);
    List<Stat> LoadAll();
}
