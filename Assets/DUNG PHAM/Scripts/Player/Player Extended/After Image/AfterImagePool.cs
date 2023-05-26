using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    public GameObject spritePrefab;
    [SerializeField] List<GameObject> sprites = new List<GameObject>();
    public Transform player;
    [SerializeField] int numberImage = 3;
    [SerializeField] float timePerImage = 0.05f;
    [SerializeField] float beginAlpha = 0.95f;
    [SerializeField] float displayTime = 0.2f;

    void Start()
    {
        AddSprites(numberImage);
    }


    void AddSprites(int number)
    {
        for (int x = 0; x < number; x++)
        {
            GameObject srToAdd = Instantiate(spritePrefab);
            srToAdd.transform.SetParent(transform);
            srToAdd.SetActive(false);

            sprites.Add(srToAdd);
        }
    }
    public void DisplaySprite()
    {
        for (int x = 0; x < sprites.Count; x++)
        {
            if (sprites[x].activeInHierarchy) sprites[x].SetActive(false);
        }

        StartCoroutine(GetSprite(0, timePerImage));
    }

    IEnumerator GetSprite(int index, float time)
    {
        if (index < sprites.Count)
        {
            sprites[index].SetActive(true);
            sprites[index].transform.position = player.transform.position;
            sprites[index].transform.localScale = player.transform.localScale;

            StartCoroutine(SpriteAlpha(sprites[index].GetComponent<SpriteRenderer>(), beginAlpha));

            StartCoroutine(DisableSprite(index, displayTime));

            yield return new WaitForSeconds(time);

            index++;
            StartCoroutine(GetSprite(index, time));
        }
    }

    IEnumerator DisableSprite(int index, float time)
    {
        yield return new WaitForSeconds(time);

        sprites[index].SetActive(false);
    }


    IEnumerator SpriteAlpha(SpriteRenderer sprite, float alpha)
    {
        sprite.color = new Color(1, 1, 1, alpha);

        yield return null;

        alpha *= alpha;

        if (alpha > 0.1f)
            StartCoroutine(SpriteAlpha(sprite, alpha));

    }
}

