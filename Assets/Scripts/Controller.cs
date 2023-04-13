using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float xDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = 10f * xDirection * Time.deltaTime;
        transform.position = transform.position + new Vector3(moveStep, 0, 0);
    }
}
