using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IdleStorageFiller : MonoBehaviour
{
    [SerializeField] float minutesToFillOne = 2f;
    [SerializeField] private Text woodText, stoneText, foodText;
    [SerializeField] private GameObject filledPanel;

    public IEnumerator Fill()
    {
        Dictionary<ResourceEnum, int> filledResources = new Dictionary<ResourceEnum, int>();
        int minutesSpent = SaveManager.GetCurrentMinutes() - SaveManager.GetData("lastSave");
        foreach (var saveIndicator in FindObjectsOfType<SaveIndicator>())
        {
            while(saveIndicator.SavedStorages == null)
                yield return null;

            foreach (Storage savedStorage in saveIndicator.SavedStorages)
            {
                int before = savedStorage.ItemCount;
                savedStorage.ItemCount = Math.Min(savedStorage.StorageCapacity, savedStorage.ItemCount + Mathf.RoundToInt(minutesSpent / minutesToFillOne));
                if (filledResources.ContainsKey(savedStorage.ResourceType))
                    filledResources[savedStorage.ResourceType] += savedStorage.ItemCount - before;
                else
                    filledResources.Add(savedStorage.ResourceType, savedStorage.ItemCount - before);
            }
        }

        if (filledResources.Any(pair => pair.Value > 0))
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
    
    void OnApplicationPause(bool pauseStatus)
    {
        if(!pauseStatus)
            StartCoroutine(Fill());
    }
}
