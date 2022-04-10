using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanPlanner : MonoBehaviour
{
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
            human.HumanTask = new GatherTask();
        }
    }

    public void OnHumanFinish(HumanController humanController)
    {
        if(humanController.HumanTask.GetType() == typeof(GatherTask))
            humanController.HumanTask = new StoreTask();
        else if(FindObjectsOfType<DroppedTarget>().FirstOrDefault(target => target.IsFree()) != null)
            humanController.HumanTask = new GatherTask();
    }
}
