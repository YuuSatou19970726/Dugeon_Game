using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject activeTarget;
    [SerializeField] bool active;
    [SerializeField] float dueTime;
    void Start()
    {
        StartCoroutine(LoadToMenu());
    }

    IEnumerator LoadToMenu()
    {
        yield return new WaitForSeconds(dueTime);

        activeTarget.SetActive(active);
    }
}
