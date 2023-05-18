using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform character;

    CultistBlueMagician cultistBlueMagician;

    float editDesiredHalfHeight = 0.7f;
    float sceneWidth = 10f;
    new Camera camera;

    [SerializeField]
    bool isMovie = false;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Start()
    {
        camera = GetComponent<Camera>();

        if (isMovie)
        {
            transform.position = new Vector3(-23f, -1f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMovie)
        {
            if (!cultistBlueMagician.GetIsFallingWater())
            {
                editDesiredHalfHeight = 1.3f;
            }
            else
            {
                editDesiredHalfHeight = 0.7f;
            }

            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = editDesiredHalfHeight * unitsPerPixel * Screen.height;

            camera.orthographicSize = desiredHalfHeight;

            transform.position = new Vector3(character.position.x, character.position.y, transform.position.z);
        } else
        {
            sceneWidth = 9f;
            editDesiredHalfHeight = 1.5f;

            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = editDesiredHalfHeight * unitsPerPixel * Screen.height;

            camera.orthographicSize = desiredHalfHeight;

            Vector2 speedVector2 = transform.position;
            speedVector2.x += 4f * Time.deltaTime;
            transform.position = new Vector3(speedVector2.x, transform.position.y, transform.position.z);
        }
    }

    public bool GetIsMovie()
    {
        return isMovie;
    }
}
