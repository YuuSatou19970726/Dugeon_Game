using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWaterController : MonoBehaviour
{
    CultistBlueMagician cultistBlueMagician;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cultistBlueMagician.GetIsFallingWater())
        {
            Destroy(gameObject);
        }
    }
}
