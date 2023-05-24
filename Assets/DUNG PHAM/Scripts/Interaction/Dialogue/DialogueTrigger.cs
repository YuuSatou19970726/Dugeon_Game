using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] KeyCode input;
    [SerializeField] Message[] messages;
    [SerializeField] Actor[] actors;
    [SerializeField] GameObject activeAfterDialog;
    string PLAYER = "Player";
    bool playerTriggered = false;

    void Update()
    {
        if (!playerTriggered) return;

        if (Input.GetKeyDown(input))
            StartDialogue();
    }

    void StartDialogue()
    {
        DialogueManager.instance.OpenConversation(messages, actors, activeAfterDialog);
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        playerTriggered = true;
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        playerTriggered = false;
    }
}

