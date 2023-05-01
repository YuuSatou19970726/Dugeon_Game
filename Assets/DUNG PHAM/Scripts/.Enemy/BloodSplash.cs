using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    public GameObject bloodPrefab;
    public float speedSplash;
    public float direction;
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q)) SpawnBlood();
    }
    public void SpawnBlood()
    {
        // int time = Random.Range(5, 20);
        // for (int i = 0; i < time; i++)
        // {
            GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            blood.transform.parent = transform;
        //     blood.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(0.1f, speedSplash) * direction, Random.Range(0.1f, speedSplash));
        //     blood.transform.rotation = Quaternion.Euler(0, Random.Range(1, 180), 0);
        //     // blood.transform.localScale = new Vector3(1, Random.Range(0.1f, 1), 1);
        // }
    }
}
