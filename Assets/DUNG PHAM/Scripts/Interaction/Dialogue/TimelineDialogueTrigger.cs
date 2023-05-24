using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineDialogueTrigger : MonoBehaviour
{
    [SerializeField] Message[] messages;
    [SerializeField] Actor[] actors;
    [SerializeField] GameObject activeAfterDialog;
    void OnEnable()
    {
        StartDialogue();
    }
    // void Update()
    // {
    //     StartDialogue();
    // }
    public void StartDialogue()
    {
        DialogueManager.instance.OpenConversation(messages, actors, activeAfterDialog);
    }
}
