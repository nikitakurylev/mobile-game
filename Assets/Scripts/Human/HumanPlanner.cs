using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanPlanner : MonoBehaviour
{
    private bool _updated = false;
    private List<StorageTarget> _freeStorageTargets;
    private List<DroppedTarget> _freeDroppedTargets;
    private List<ResourceTarget> _freeResourceTargets;
    private BuildTarget _buildTarget;
    private static HumanPlanner _instance;
    private HashSet<HumanController> _humanControllers;

    public static HumanPlanner Instance => _instance;

    private void OnValidate()
    {
        if (FindObjectsOfType<HumanPlanner>().FirstOrDefault(planner => planner != this) != null)
        {
            Destroy(this);
            throw new UnityException("Only one Human Planner per scene");
        }
    }

    HumanPlanner()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    private void Awake()
    {
        _humanControllers = new HashSet<HumanController>();
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
        if (!_humanControllers.Contains(humanController))
        {
            _humanControllers.Add(humanController);
        }
        if(_buildTarget == null)
            _buildTarget = FindObjectOfType<BuildTarget>();
        if (_buildTarget != null && !_buildTarget.Active)
            _buildTarget = null;

        if (_buildTarget != null)
        {
            humanController.EnqueueTask(new BuildTask(_buildTarget));
        }
        else if(humanController.InventoryResource != ResourceEnum.None)
        {
            List<StorageTarget> freeStorageTargetsOfType = FindObjectsOfType<StorageTarget>()
                .Where(target => target.Resource == humanController.InventoryResource && target.GetFreeSpace() > 0).ToList();
            if (freeStorageTargetsOfType.Any())
            {
                StorageTarget target = freeStorageTargetsOfType.Aggregate((i1, i2) => i1.Priority < i2.Priority ? i1 : i2);
                humanController.EnqueueTask(new StoreTask(target, Math.Min(humanController.InventoryCount, target.GetFreeSpace())));
            }
            else
            {
                humanController.EnqueueTask(new IdleTask());
            }
        }
        else
        {
            if (!_updated)
            {
                _freeStorageTargets = FindObjectsOfType<StorageTarget>().Where(target => target.GetFreeSpace() > 0)
                    .ToList();
                _freeDroppedTargets = FindObjectsOfType<DroppedTarget>()
                    .Where(target => target.GetAvailableResources() > 0)
                    .ToList();
                _freeResourceTargets = FindObjectsOfType<ResourceTarget>()
                    .Where(target => target.GetAvailableResources() > 0).ToList();
                _updated = true;
            }

            if (_freeStorageTargets.Count > 0)
            {
                Vector3 humanPos = humanController.transform.position;
                StorageTarget storageTarget =
                    _freeStorageTargets.Aggregate((i1, i2) => i1.Priority < i2.Priority ? i1 : i2);
                //humanController.InventoryResource = storageTarget.Resource;
                List<DroppedTarget> droppedTargets =
                    _freeDroppedTargets.Where(target =>
                            target.Resource == storageTarget.Resource && target.GetAvailableResources() > 0)
                        .OrderBy(target => target.Priority).ThenBy(target =>
                            (target.transform.position - humanPos).sqrMagnitude).ToList();
                int targetCount = Math.Min(humanController.InventoryCapacity(storageTarget.Resource), storageTarget.GetFreeSpace());

                bool isTaskChosen = false;
                if (droppedTargets.Count > 0)
                {
                    int droppedTargetsToCollect = targetCount;
                    for (int i = 0; i < droppedTargets.Count; i++)
                    {
                        if (storageTarget.gameObject == droppedTargets[i].gameObject)
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
                        humanController.EnqueueTask(new StoreTask(storageTarget,
                            droppedTargetsToCollect - targetCount));
                        if (storageTarget.GetFreeSpace() <= 0)
                            _freeStorageTargets.Remove(storageTarget);
                    }
                }

                if (!isTaskChosen)
                {
                    ResourceTarget resourceTarget = null;
                    ResourceTarget[] neededResources = _freeResourceTargets
                        .Where(target => target.Resource == storageTarget.Resource).ToArray();
                    if(neededResources.Any())
                        resourceTarget = neededResources.Aggregate((i1, i2) =>
                        {
                            return (i1.transform.position - humanPos).sqrMagnitude <
                                   (i2.transform.position - humanPos).sqrMagnitude
                                ? i1
                                : i2;
                        });
                    if (resourceTarget)
                    {
                        targetCount = Math.Min(targetCount, resourceTarget.GetAvailableResources());
                        humanController.EnqueueTask(new HarvestTask(resourceTarget, targetCount));
                        if (resourceTarget.GetAvailableResources() <= 0)
                            _freeResourceTargets.Remove(resourceTarget);
                    }
                    else
                        humanController.EnqueueTask(new IdleTask());
                }
            }
            else
                humanController.EnqueueTask(new IdleTask());
        }
    }

    public static void CancelAll()
    {
        foreach (HumanController humanController in _instance._humanControllers)
            humanController.CancelAll();
    }
}