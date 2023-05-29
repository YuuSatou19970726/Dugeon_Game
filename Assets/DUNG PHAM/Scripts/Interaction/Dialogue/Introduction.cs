using System.Collections;
using UnityEngine;
public class Introduction : MonoBehaviour
{
    [TextArea][SerializeField] string introMessage;
    [SerializeField] int minScale = 50;
    [SerializeField] int maxScale = 70;
    bool isTriggered = false;
    bool isShowed;
    string PLAYER = "Player";

    void Update()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 3f);

        if (isTriggered && !isShowed)
        {
            DialogueManager.instance.ShowIntroduction(introMessage, position, minScale, maxScale);
            isShowed = true;
        }
        else if (isTriggered && isShowed)
        {
            DialogueManager.instance.PositionUpdate(position);
        }
        else if (!isTriggered && isShowed)
        {
            DialogueManager.instance.CloseIntroduction();
            isShowed = false;
        }
    }


    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        isTriggered = true;
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        isTriggered = false;
    }
}
