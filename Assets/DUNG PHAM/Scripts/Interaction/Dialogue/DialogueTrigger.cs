using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    bool playerTrigger = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerTrigger)
            StartDialogue();
    }

    public void StartDialogue()
    {
        DialogueManager.instance.OpenConversation(messages, actors);
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag("Player")) playerTrigger = true;
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag("Player")) playerTrigger = false;
    }
}


[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}


[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
