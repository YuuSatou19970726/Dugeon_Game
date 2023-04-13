using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material material;
    float distance = 0.2f;

    [Range(0f, 0.5f)]
    float speed = 0.2f;

    private void Start() {
        material = GetComponent<Renderer>().material;
    }

    private void Update() {
        distance += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex" ,Vector2.right * distance);
    }
}
