using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PulseAttack : MonoBehaviour
{

    float speedMove = 7f;

    private void Start()
    {
        transform.Rotate(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        PulseAttackMovement();

        if (transform.position.y > 15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void PulseAttackMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = speedMove + Random.Range(1f, 3f);
        speedVector2.y += speedRandom * Time.deltaTime;
        transform.position = speedVector2;
    }
}
