using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTools : MonoBehaviour
{
    [SerializeField]
    bool isRotate = false;

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, 360 * 13 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
