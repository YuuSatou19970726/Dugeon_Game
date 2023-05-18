using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private Transform cameraTranform;
    [Range(0, 2)] public float parallaxEffectX, parallaxEffectY;
    private Vector3 lastPosition;
    [SerializeField] Transform playerTransform;
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovementController>().transform;

        lastPosition = playerTransform.position;
    }
    void Update()
    {
        ParallaxEffect();
    }

    void ParallaxEffect()
    {
        Vector3 cameraDistance = playerTransform.position - lastPosition;
        Vector3 backgroundDistance = new Vector3(cameraDistance.x * parallaxEffectX, cameraDistance.y * parallaxEffectY, transform.position.z);
        // Vector3 newPosition = transform.position + backgroundDistance;

        // transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.1f);
        transform.position += backgroundDistance;

        lastPosition = playerTransform.position;
    }
}
