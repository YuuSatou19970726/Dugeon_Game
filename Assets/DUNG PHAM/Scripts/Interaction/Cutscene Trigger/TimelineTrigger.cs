using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineTrigger : MonoBehaviour
{
    [SerializeField] int index;
    public KeyCode input;
    string PLAYER = "Player";
    [HideInInspector] public bool playerOverTrigger;

    void Update()
    {
        TriggerTimeline();
    }

    void TriggerTimeline()
    {
        if (!playerOverTrigger) return;

        if (input != KeyCode.None)
            if (!Input.GetKeyDown(input)) return;

        TimelineObserver.instance.TimelineTriggerMethod(index);
        this.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        playerOverTrigger = true;
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        playerOverTrigger = false;
    }
}
