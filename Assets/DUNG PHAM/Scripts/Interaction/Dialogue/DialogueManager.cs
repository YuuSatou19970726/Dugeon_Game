using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Image actorAvatar;
    [SerializeField] Text actorName;
    [SerializeField] Text messageText;
    [SerializeField] RectTransform dialogueBox;
    [SerializeField] TextMeshProUGUI introductionTextMesh;
    [SerializeField] bool inConversation = false;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    GameObject activeAfterDialog;

    #region SINGLETON PATTERN
    public static DialogueManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region MONOBEHAVIOUS
    void Start()
    {
        dialogueBox.gameObject.SetActive(false);

        introductionTextMesh.enabled = false;
    }

    void Update()
    {
        if (!inConversation) return;

        InputControllerNew.instance.canInput = false;

        if (Input.GetMouseButtonDown(0))
            NextMessage();
    }
    #endregion

    #region DIALOGUE
    public void OpenConversation(Message[] messages, Actor[] actors, GameObject activeAfterDialog)
    {
        actors[0].sprite = FindObjectOfType<PlayerDatabase>().avatar;
        inConversation = true;

        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        DisplayMessage();

        this.activeAfterDialog = activeAfterDialog;
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

            if (activeAfterDialog)
                activeAfterDialog.SetActive(true);
        }
    }
    #endregion

    #region INTRODUCTION
    public void ShowIntroduction(string introMessage, Vector2 position, int minScale, int maxScale)
    {
        ShowDialogue(introMessage, position);

        StartCoroutine(TransformIntroduction(0.5f, minScale, maxScale));
    }

    public void PositionUpdate(Vector2 position)
    {
        introductionTextMesh.transform.position = position;
    }

    void ShowDialogue(string introMessage, Vector2 position)
    {
        introductionTextMesh.enabled = true;
        introductionTextMesh.text = introMessage;
        introductionTextMesh.transform.position = position;
    }

    public void CloseIntroduction()
    {
        introductionTextMesh.enabled = false;

        StopAllCoroutines();
    }

    IEnumerator TransformIntroduction(float delayTime, int minScale, int maxScale)
    {
        introductionTextMesh.fontSize = minScale;

        yield return new WaitForSeconds(delayTime);

        introductionTextMesh.fontSize = maxScale;

        yield return new WaitForSeconds(delayTime);

        StartCoroutine(TransformIntroduction(delayTime, minScale, maxScale));
    }
    #endregion
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
