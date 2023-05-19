using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] Image actorAvatar;
    [SerializeField] Text actorName;
    [SerializeField] Text messageText;
    [SerializeField] RectTransform dialogueBox;
    public Text introductionText;
    [SerializeField] public bool inConversation = false;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dialogueBox.gameObject.SetActive(false);
        introductionText.enabled = false;
    }

    void Update()
    {
        if (!inConversation) return;

        InputControllerNew.instance.canInput = false;

        if (Input.GetMouseButtonDown(0))
            NextMessage();
    }

    public void OpenConversation(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        inConversation = true;

        DisplayMessage();
    }

    void DisplayMessage()
    {
        dialogueBox.gameObject.SetActive(true);

        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorAvatar.sprite = actorToDisplay.sprite;
    }

    void NextMessage()
    {
        activeMessage++;

        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            dialogueBox.gameObject.SetActive(false);
            InputControllerNew.instance.canInput = true;

            inConversation = false;
        }
    }
}
