using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    string PLAYER = "Player";
    bool playerTriggered = false;

    void Update()
    {
        if (InputControllerNew.instance.isInteractPress && playerTriggered)
            StartDialogue();
    }

    public void StartDialogue()
    {
        DialogueManager.instance.OpenConversation(messages, actors);
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER))
            playerTriggered = true;
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER))
            playerTriggered = false;
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
