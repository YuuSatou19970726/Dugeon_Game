using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform character;

    CultistBlueMagician cultistBlueMagician;

    [SerializeReference]
    float editDesiredHalfHeight = 0.7f;

    float sceneWidth = 10f;
    Camera camera;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cultistBlueMagician.GetIsFallingWater())
        {
            editDesiredHalfHeight = 1.3f;
        } else
        {
            editDesiredHalfHeight = 0.7f;
        }

        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = editDesiredHalfHeight * unitsPerPixel * Screen.height;

        camera.orthographicSize = desiredHalfHeight;

        transform.position = new Vector3(character.position.x, character.position.y, transform.position.z);
    }
}
