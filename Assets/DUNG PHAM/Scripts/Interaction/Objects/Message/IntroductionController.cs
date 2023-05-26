using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class IntroductionController : MonoBehaviour
{
    Dictionary<int, string> message = new Dictionary<int, string>();
    public TextMeshProUGUI text;
    public GameObject panel;

    void Start()
    {
        Message();
        CloseMessage();
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


    void Message()
    {
        message.Add(0, "Hello");
        message.Add(1, "Goobye");
    }
}
