using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] GameObject waterEffectPrefab;
    AudioSource audioSrc;
    List<GameObject> waterEffects = new List<GameObject>();
    string PLAYER = "Player";
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSrc.Stop();
        InitWaterEffect(10);
    }
    void Update()
    {
        WaterEffectReset();

    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        WaterImpactEffect(coli.transform.position);
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (!coli.CompareTag(PLAYER)) return;

        WaterImpactEffect(coli.transform.position);
    }

    void WaterImpactEffect(Vector2 position)
    {
        audioSrc.Play();

        Vector2 place = new Vector2(position.x, position.y - 2);

        foreach (GameObject w in waterEffects)
        {
            if (w.activeInHierarchy) continue;
            w.transform.position = place;
            w.SetActive(true);

            break;
        }
    }

    void WaterEffectReset()
    {
        foreach (GameObject w in waterEffects)
        {
            if (w.GetComponent<ParticleSystem>().isPlaying) return;

            w.SetActive(false);
        }
    }

    void InitWaterEffect(int number)
    {
        for (int x = 0; x < number; x++)
        {
            GameObject water = Instantiate(waterEffectPrefab);
            water.transform.parent = transform.parent;
            water.SetActive(false);

            waterEffects.Add(water);
        }
    }
}
