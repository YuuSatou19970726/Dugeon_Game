using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private Transform cameraTranform;
    [Range(0, 2)] public float parallaxEffectX, parallaxEffectY;
    private Vector3 lastCameraPositon;
    private float textureUnitSizeX;

    void Start()
    {
        cameraTranform = Camera.main.transform;
        lastCameraPositon = cameraTranform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    void Update()
    {
        Vector3 distance = cameraTranform.position - lastCameraPositon;
        transform.position += new Vector3(distance.x * parallaxEffectX, distance.y * parallaxEffectY, transform.position.z);
        lastCameraPositon = cameraTranform.position;

        // MoveBackground();
    }
    void MoveBackground()
    {
        if (Mathf.Abs(cameraTranform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTranform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTranform.position.x + offsetPositionX, transform.position.y);
        }
    }

}
