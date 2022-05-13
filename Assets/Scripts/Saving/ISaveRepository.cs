using System.Collections.Generic;

public interface ISaveRepository
{
    void Save(string key, int value);
    int Load(string key);
    List<Stat> LoadAll();
}
