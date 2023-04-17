using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillerZone : MonoBehaviour
{
    float speedMove = 3f;
    // Update is called once per frame
    void Update()
    {
        KillerMovement();
    }

    void KillerMovement()
    {
        Vector2 vector2 = transform.position;
        vector2.x += speedMove * Time.deltaTime;
        transform.position = vector2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
