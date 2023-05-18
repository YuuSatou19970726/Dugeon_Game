using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    Animator animator;
    TimelineTrigger timelineTrigger;
    string clipName = "Trigger";
    void Awake()
    {
        timelineTrigger = GetComponent<TimelineTrigger>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    void Update()
    {
        if (timelineTrigger.input != KeyCode.None && Input.GetKeyDown(timelineTrigger.input) && timelineTrigger.playerOverTrigger)
        {
            animator.enabled = true;
            animator.Play(clipName);
        }
    }
}
