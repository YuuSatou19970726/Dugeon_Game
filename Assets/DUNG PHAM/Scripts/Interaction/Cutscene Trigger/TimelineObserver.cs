using UnityEngine;
using System;

public class TimelineObserver : MonoBehaviour
{
    public static TimelineObserver instance;


    public event Action<int> OnTimelineTrigger;
    public void TimelineTriggerMethod(int index)
    {
        OnTimelineTrigger?.Invoke(index);
    }

    void Awake()
    {
        instance = this;
    }

}
