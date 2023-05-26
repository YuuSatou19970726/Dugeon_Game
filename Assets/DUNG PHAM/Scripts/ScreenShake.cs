using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public float duration = 0.5f;
    public float amplitude = 5f;
    public float frequency = 1f;
    [SerializeField] Transform playerTranform;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        CameraFollowPlayer();
    }

    void CameraFollowPlayer()
    {
        Vector2 cameraPosition = transform.position;
        Vector2 playerPosition = playerTranform.position;

        transform.position = Vector2.MoveTowards(cameraPosition, playerPosition, 0.1f);
    }

    public void ShakeCamera()
    {
        StartCoroutine(CameraShaking());
    }
    IEnumerator CameraShaking()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
    }
}
