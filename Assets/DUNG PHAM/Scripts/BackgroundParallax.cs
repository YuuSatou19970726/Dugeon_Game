using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    Transform cameraTranform;
    [Range(0, 1)] public float parallaxEffectX, parallaxEffectY;
    Vector3 lastPosition;
    Vector3 startPosition;
    void Start()
    {
        cameraTranform = Camera.main.transform;

        startPosition = transform.position;
        // lastPosition = transform.position;
    }
    void FixedUpdate()
    {
        Vector3 cameraDistance = new Vector3(cameraTranform.position.x * parallaxEffectX, cameraTranform.position.y * parallaxEffectY) + startPosition;
        transform.position = new Vector3(cameraDistance.x, cameraDistance.y, 0);
    }
    void Update()
    {
        // ParallaxEffect();
    }

    void ParallaxEffect()
    {
        Vector3 cameraDistance = cameraTranform.position - lastPosition;
        Vector3 backgroundDistance = new Vector3(cameraDistance.x * parallaxEffectX, cameraDistance.y * parallaxEffectY, transform.position.z);
        // Vector3 newPosition = transform.position + backgroundDistance;

        // transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.1f);
        transform.position += backgroundDistance;

        lastPosition = transform.position;
    }
}
