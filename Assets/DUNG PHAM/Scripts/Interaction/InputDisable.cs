using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisable : MonoBehaviour
{
    string PLAYER_TAG = "Player";
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER_TAG)) return;

        coli.GetComponent<InputControllerNew>().canInput = false;
        // coli.GetComponent<PlayerStateManager>().SwitchState(GetComponent<PlayerStateManager>().idleState);
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER_TAG)) return;

        coli.GetComponent<InputControllerNew>().canInput = true;

    }
}
