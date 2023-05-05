using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerController : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, .9f);
    }
}
