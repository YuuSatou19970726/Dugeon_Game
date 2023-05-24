using System.Collections;
using UnityEngine;
public class Introduction : MonoBehaviour
{
    [SerializeField] string introMessage;
    [SerializeField] int minScale = 50;
    [SerializeField] int maxScale = 70;
    bool isTriggered = false;
    bool isShowed;

    void Update()
    {
        if (isTriggered)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 3f);

            if (isShowed)
            {
                DialogueManager.instance.PositionUpdate(position);
                return;
            }

            DialogueManager.instance.ShowIntroduction(introMessage, position, minScale, maxScale);

            isShowed = true;
        }
        else
        {
            DialogueManager.instance.CloseIntroduction();
        }
    }


    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag("Player")) return;

        isTriggered = true;
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (!coli.CompareTag("Player")) return;

        isTriggered = false;
        isShowed = false;
    }
}
