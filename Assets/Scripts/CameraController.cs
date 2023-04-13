using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform character;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.position.x, character.position.y, transform.position.z);
    }
}
