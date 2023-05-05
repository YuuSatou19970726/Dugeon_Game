using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterPillar : MonoBehaviour
{
    [SerializeField]
    GameObject fallingWater;

    BoxCollider2D boxCollider2D;

    CultistBlueMagician cultistBlueMagician;
    bool isLoop = false;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cultistBlueMagician.GetIsFallingWater() && !isLoop)
        {
            StartCoroutine(SpawnFallingWater());
            isLoop = true;
        }

        if (!cultistBlueMagician.GetIsFallingWater())
        {
            boxCollider2D.isTrigger = true;
        }
    }

    IEnumerator SpawnFallingWater()
    {
        yield return new WaitForSeconds(.3f);

        boxCollider2D.isTrigger = false;

        Vector2 position = transform.position;
        position.y += 6f;
        Instantiate(fallingWater, position, Quaternion.identity);
    }
}
