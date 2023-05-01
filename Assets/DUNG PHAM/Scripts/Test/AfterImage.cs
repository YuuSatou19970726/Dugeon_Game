using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://forum.unity.com/threads/how-to-create-ghost-or-after-image-on-an-animated-sprite.334079/

public class AfterImage : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("SpawnTrailPart", 0, 0.2f); // replace 0.2f with needed repeatRate
    }

    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        trailPart.transform.localScale = transform.localScale; // We forgot about this line!!!
        // trailParts.Add(trailPart);

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        Destroy(trailPart, 0.5f); // replace 0.5f with needed lifeTime
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }
}
