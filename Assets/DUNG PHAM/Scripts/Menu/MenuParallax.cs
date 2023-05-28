using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    Vector2 startPos;
    [SerializeField] int moveModifierX;
    [SerializeField] int moveModifierY;

    void Start()
    {
        startPos = transform.position;

    }
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, startPos.x + (mousePos.x * moveModifierX), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, startPos.y + (mousePos.y * moveModifierY), 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}
