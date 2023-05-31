using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        audioSource.volume = GameManager.instance.SetVolume();
        
        if (!audioSource.isPlaying)
            PlayMusic();
    }
    void PlayMusic()
    {
        int index = GetRandomClip();
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
    int GetRandomClip()
    {
        return Random.Range(0, audioClips.Length);
    }
}
