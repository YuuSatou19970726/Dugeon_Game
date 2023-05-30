using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieState : IState
{
    float timer;
    PlayerStateManager player;
    public PlayerDieState(PlayerStateManager player)
    {
        this.player = player;
    }


    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Die");

        timer = 0f;
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        timer += Time.deltaTime;

        if (timer > 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
