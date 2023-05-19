using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarGetter : MonoBehaviour
{
    [SerializeField] public Sprite avatar;

    void Awake()
    {
        avatar = GetComponent<SpriteRenderer>().sprite;
    }
}
