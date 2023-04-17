using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistBlueMagician : MonoBehaviour
{
    bool isFallingWater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsFallingWater(bool isFallingWater)
    {
        this.isFallingWater = isFallingWater;
    }

    public bool GetIsFallingWater()
    {
        return isFallingWater;
    }
}
