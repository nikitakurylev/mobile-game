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
            human.EnqueueTask(new IdleTask());
        }
    }

    public void OnHumanFinish(HumanController humanController)
    {
        humanController.EnqueueTask(new IdleTask());
    }
}
