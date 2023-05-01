using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroductionController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject panel;
    bool showMsg;
    void Start()
    {
        CloseMessage();
    }

    void Update()
    {
        if (showMsg) ShowMessage(0);
    }

    public void ShowMessage(int index)
    {
        panel.SetActive(true);
        text.enabled = true;
        text.text = message[index];
    }

    public void CloseMessage()
    {
        panel.SetActive(false);
        text.enabled = false;
    }

    string[] message ={
        "Hello",
        "Goodbye"
    };
}
