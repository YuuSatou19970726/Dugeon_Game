using UnityEngine;
public class Introduction : MonoBehaviour
{
    public string introMessage = "Hello World";

    bool isTrigger = false;
    void Update()
    {
        if (!isTrigger) return;

        ShowIntroduction();
    }

    void ShowIntroduction()
    {
        DialogueManager.instance.introductionText.transform.position = new Vector3(transform.position.x, transform.position.y + 2f);
        DialogueManager.instance.introductionText.enabled = true;
        DialogueManager.instance.introductionText.text = introMessage;
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag("Player")) return;

        isTrigger = true;
    }



    void OnTriggerExit2D(Collider2D coli)
    {
        DialogueManager.instance.introductionText.enabled = (false);

        isTrigger = false;
    }
}
