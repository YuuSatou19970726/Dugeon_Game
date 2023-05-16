using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Image actorAvatar;
    public Text actorName;
    public Text messageText;
    public RectTransform dialogueBox;
    public Text introductionText;
    bool inConversation = false;

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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inConversation) NextMessage();

        if (inConversation)
        {
            InputControllerNew.instance.canInput = false;
        }
        else
        {
            InputControllerNew.instance.canInput = true;
        }
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
            inConversation = false;
        }
    }
}
