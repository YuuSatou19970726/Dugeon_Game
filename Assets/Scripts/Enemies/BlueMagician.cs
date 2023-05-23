using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMagician : MonoBehaviour
{

    [SerializeField]
    GameObject pulseAttack;

    [SerializeField]
    GameObject coinBlue;

    CultistBlueMagician cultistBlueMagician;

    int spawnCount = 4;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSkill());
        StartCoroutine(SpawnCoinBlue());
    }

    private void Update()
    {
        if (!cultistBlueMagician.GetIsFallingWater())
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnSkill()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));

        Vector2 bodyPosition = transform.position;
        bodyPosition.y -= 6f;
        bodyPosition.x += Random.Range(-6f, 6f);

        Instantiate(pulseAttack, bodyPosition, Quaternion.identity);

        if(spawnCount > 0)
        {
            spawnCount--;
            StartCoroutine(SpawnSkill());
        }
    }

    IEnumerator SpawnCoinBlue()
    {
        yield return new WaitForSeconds(3f);

        Vector2 vector2Position = new Vector2(Random.Range(14.5f,26.5f), Random.Range(-1.5f, -0.3f));

        Instantiate(coinBlue, vector2Position, Quaternion.identity);
    }


}
