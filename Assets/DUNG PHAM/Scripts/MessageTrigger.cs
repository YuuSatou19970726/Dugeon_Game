using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    IntroductionController introductionController;
    public int index;
    string PLAYER = "Player";
    void Awake()
    {
        introductionController = FindObjectOfType<IntroductionController>();
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER)) introductionController.ShowMessage(index);
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER)) introductionController.CloseMessage();
    }
}
