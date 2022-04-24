using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanPlanner : MonoBehaviour
{
    private bool _updated = false;
    private List<StorageTarget> _freeStorageTargets;
    private List<DroppedTarget> _freeDroppedTargets;
    private List<ResourceTarget> _freeResourceTargets;

    private void OnValidate()
    {
        if (FindObjectsOfType<HumanPlanner>().FirstOrDefault(planner => planner != this) != null)
        {
            Destroy(this);
            throw new UnityException("Only one Human Planner per scene");
        }
    }

    private void Start()
    {
        foreach (var human in FindObjectsOfType<HumanController>())
        {
            human.EnqueueTask(new IdleTask());
        }
    }

    private void Update()
    {
        _updated = false;
    }

    public void OnHumanFinish(HumanController humanController)
    {
        if (!_updated)
        {
            _freeStorageTargets =
                FindObjectsOfType<StorageTarget>().Where(target => target.GetFreeSpace() > 0).OrderBy((target => target.Priority)).ToList();
            _freeDroppedTargets = FindObjectsOfType<DroppedTarget>().Where(target => target.GetAvailableResources() > 0)
                .ToList();
            _freeResourceTargets = FindObjectsOfType<ResourceTarget>()
                .Where(target => target.GetAvailableResources() > 0).ToList();
            _updated = true;
        }

        if (_freeStorageTargets.Count > 0)
        {
            StorageTarget storageTarget = _freeStorageTargets[0];
            List<DroppedTarget> droppedTargets =
                _freeDroppedTargets.Where(target => target.Resource == storageTarget.Resource)
                    .OrderBy(target => target.Priority).ThenBy(target =>
                        (target.transform.position - humanController.transform.position).sqrMagnitude).ToList();
            int targetCount = Math.Min(humanController.InventoryCapacity, storageTarget.GetFreeSpace());
            List<ResourceTarget> resourceTargets = _freeResourceTargets
                .Where(target => target.Resource == storageTarget.Resource)
                .OrderByDescending(target => target.GetAvailableResources()).ToList();
            bool isTaskChosen = false;
            if (droppedTargets.Count > 0)
            {
                int droppedTargetsToCollect = targetCount;
                for (int i = 0; i < droppedTargets.Count; i++)
                {
                    if(storageTarget.gameObject == droppedTargets[i].gameObject)
                        continue;
                    isTaskChosen = true;
                    int amountToOccupy = Math.Min(targetCount, droppedTargets[i].GetAvailableResources());
                    humanController.EnqueueTask(new GatherTask(droppedTargets[i], amountToOccupy));
                    if (droppedTargets[i].GetAvailableResources() <= 0)
                        _freeDroppedTargets.Remove(droppedTargets[i]);
                    targetCount -= amountToOccupy;
                    if (targetCount <= 0)
                        break;
                }

                if (isTaskChosen)
                {
                    humanController.EnqueueTask(new StoreTask(storageTarget, droppedTargetsToCollect - targetCount));
                    if (storageTarget.GetFreeSpace() <= 0)
                        _freeStorageTargets.Remove(storageTarget);
                }
            }

            if (!isTaskChosen)
            {
                if (resourceTargets.Count > 0)
                {
                    targetCount = Math.Min(targetCount, resourceTargets[0].GetAvailableResources());
                    humanController.EnqueueTask(new HarvestTask(resourceTargets[0], targetCount));
                    if (resourceTargets[0].GetAvailableResources() <= 0)
                        _freeResourceTargets.Remove(resourceTargets[0]);
                }
                else
                    humanController.EnqueueTask(new IdleTask());
            }
        }
        else
            humanController.EnqueueTask(new IdleTask());
    }
}