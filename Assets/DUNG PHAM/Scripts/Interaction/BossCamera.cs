using UnityEngine;

public class BossCamera : MonoBehaviour
{
    [SerializeField] GameObject bossCamera;
    string PLAYER = "Player";
    bool isTriggered;

    void Update()
    {
        bossCamera.SetActive(isTriggered);
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        isTriggered = true;
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        isTriggered = false;
    }
}
