using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    MainGame mainGame;
    CultistBlueMagician cultistBlueMagician;

    float editDesiredHalfHeight = 0.7f;
    float sceneWidth = 10f;
    Camera camera;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Start()
    {
        camera = GetComponent<Camera>();

        if (mainGame.GetIsMovie())
        {
            transform.position = new Vector3(-23f, 2.5f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainGame.GetIsMovie())
        {
            editDesiredHalfHeight = 1.95f;

            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = editDesiredHalfHeight * unitsPerPixel * Screen.height;

            camera.orthographicSize = desiredHalfHeight;

            Vector2 speedVector2 = transform.position;
            speedVector2.x += 4f * Time.deltaTime;

            if (transform.position.x > 120f)
            {
                cultistBlueMagician.SetIsFallingWater(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                transform.position = new Vector3(speedVector2.x, transform.position.y, transform.position.z);
            }
        }
    }

    public void SetMoveCamera (Transform transform)
    {
        if (!mainGame.GetIsMovie())
        {
            if (!cultistBlueMagician.GetIsFallingWater())
            {
                editDesiredHalfHeight = 0.95f;
            }
            else
            {
                editDesiredHalfHeight = 1.5f;
            }

            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = editDesiredHalfHeight * unitsPerPixel * Screen.height;

            camera.orthographicSize = desiredHalfHeight;
            this.transform.position = new Vector3(transform.position.x, transform.position.y, this.transform.position.z);
        }
    }
}
