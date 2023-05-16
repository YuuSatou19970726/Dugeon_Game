using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingGravitation : MonoBehaviour
{

    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(KinematicGravitation());
        }

        if (collision.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator KinematicGravitation()
    {
        yield return new WaitForSeconds(5f);
        rigid.isKinematic = false;
    }
}
