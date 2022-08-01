using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IdleStorageFiller : MonoBehaviour
{
    [SerializeField] float hoursToFullyFill = 12f;
    [SerializeField] private Text woodText, stoneText, foodText;
    [SerializeField] private GameObject filledPanel;

    public IEnumerator Fill()
    {
        Dictionary<ResourceEnum, int> filledResources = new Dictionary<ResourceEnum, int>();
        float hoursSpent = (unchecked((int) DateTime.Now.Ticks) - SaveManager.GetData("lastSave")) * 1f / TimeSpan.TicksPerHour;
        foreach (var saveIndicator in FindObjectsOfType<SaveIndicator>())
        {
            while(saveIndicator.SavedStorages == null)
                yield return null;

            foreach (Storage savedStorage in saveIndicator.SavedStorages)
            {
                int before = savedStorage.ItemCount;
                savedStorage.ItemCount = Math.Min(savedStorage.StorageCapacity, savedStorage.ItemCount + Mathf.RoundToInt(savedStorage.StorageCapacity * hoursSpent / hoursToFullyFill));
                if (filledResources.ContainsKey(savedStorage.ResourceType))
                    filledResources[savedStorage.ResourceType] += savedStorage.ItemCount - before;
                else
                    filledResources.Add(savedStorage.ResourceType, savedStorage.ItemCount - before);
            }
        }

        if (filledResources.Any())
        {
            filledPanel.SetActive(true);
            if (filledResources.ContainsKey(ResourceEnum.Wood))
                woodText.text = filledResources[ResourceEnum.Wood].ToString();
            if (filledResources.ContainsKey(ResourceEnum.Stone))
                stoneText.text = filledResources[ResourceEnum.Stone].ToString();
            if (filledResources.ContainsKey(ResourceEnum.Food))
                foodText.text = filledResources[ResourceEnum.Food].ToString();
        }
    }
}
