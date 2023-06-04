using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform mainCamera;

    Vector3 cameraStartPosition;
    float distance; //distance between the camera start position and its current position

    GameObject[] backgrounds;
    Material[] materials;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    float parallaxSpeed = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
        cameraStartPosition = mainCamera.position;

        int backCount = transform.childCount;
        materials = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // find the farthest background
        {
            if ((backgrounds[i].transform.position.z - mainCamera.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - mainCamera.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) // set the speed of background
            {
                backSpeed[i] = 1 - (backgrounds[i].transform.position.z - mainCamera.position.z) / farthestBack;
            }
    }
    
    private void LateUpdate() {
        distance = mainCamera.position.x - cameraStartPosition.x;
        transform.position = new Vector3(mainCamera.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);   
        }
    }
}
