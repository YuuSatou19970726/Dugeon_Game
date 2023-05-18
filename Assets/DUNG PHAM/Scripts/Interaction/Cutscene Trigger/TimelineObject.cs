using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineObject : MonoBehaviour
{
    PlayableDirector playableDirector;
    [SerializeField] int index;
    [SerializeField] float delayTime = 5;

    void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    void Start()
    {
        TimelineObserver.instance.OnTimelineTrigger += CutscenePlay;
    }


    void CutscenePlay(int index)
    {
        if (index == this.index)
        {
            playableDirector.Play();

            StartCoroutine(DisableObjectDelay(delayTime));
        }
    }
    IEnumerator DisableObjectDelay(float delayTime)
    {
        InputControllerNew.instance.canInput = false;

        yield return new WaitForSeconds(delayTime);

        InputControllerNew.instance.canInput = true;

        gameObject.SetActive(false);
    }
}
