using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Image actorAvatar;
    [SerializeField] Text actorName;
    [SerializeField] Text messageText;
    [SerializeField] RectTransform dialogueBox;
    public Text introductionText;
    [SerializeField] bool inConversation = false;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;


    #region Singleton Pattern
    public static DialogueManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion


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
        actors[0].sprite = FindObjectOfType<AvatarGetter>().avatar;
        inConversation = true;

        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

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

[System.Serializable]
public class Message
{
    public int actorId;
    [TextArea] public string message;
}


[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
