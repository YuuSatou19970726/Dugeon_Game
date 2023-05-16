using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PulseAttack : MonoBehaviour
{

    float speedMove = 3f;
    CultistBlueMagician cultistBlueMagician;

    float positionY;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Start()
    {
        positionY = transform.position.y + 3f;
        transform.Rotate(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        PulseAttackMovement();

        if (cultistBlueMagician.GetIsFallingWater())
        {
            if (transform.position.y > 13f)
                ReStart();
        } else
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

    void ReStart()
    {
        float positionX = 20f + Random.Range(-6f, 6f);
        transform.position = new Vector2(positionX, positionY);
    }

    void PulseAttackMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = speedMove + Random.Range(1f, 3f);
        speedVector2.y += speedRandom * Time.deltaTime;
        transform.position = speedVector2;
    }
}
