using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisable : MonoBehaviour
{
    string PLAYER_TAG = "Player";
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER_TAG))
        {
            coli.GetComponent<InputControllerNew>().inputX = 0;
            coli.GetComponent<InputControllerNew>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER_TAG))
        {
            coli.GetComponent<InputControllerNew>().enabled = true;
        }
    }
}
