using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject activeTarget;
    [SerializeField] bool active;
    [SerializeField] float dueTime;
    float timer;
    void Start()
    {
        timer = 0f;
        activeTarget.SetActive(!active);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > dueTime)
            activeTarget.SetActive(active);
    }
}
