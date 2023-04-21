using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyBlood());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator DestroyBlood()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
