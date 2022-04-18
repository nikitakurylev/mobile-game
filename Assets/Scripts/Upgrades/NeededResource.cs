using UnityEngine;

[System.Serializable]
public class NeededResource
{
    [SerializeField] private ResourceEnum _resourceType;

    public ResourceEnum ResourceType => _resourceType;

    public int Amount => _amount;

    [SerializeField] private int _amount;
}
