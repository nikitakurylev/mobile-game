using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private GameObject _upgradePanelPrefab;

    private void OnValidate()
    {
        if (_upgradePanelPrefab?.GetComponent<UpgradePanel>() == null)
            throw new UnityException("Assigned Prefab without Upgrade Panel");
    }

    private void Start()
    {
        foreach (Upgrade upgrade in _upgrades)
        {
            Instantiate(_upgradePanelPrefab, transform).GetComponent<UpgradePanel>().SetUpgrade(upgrade);
        }
    }
}
