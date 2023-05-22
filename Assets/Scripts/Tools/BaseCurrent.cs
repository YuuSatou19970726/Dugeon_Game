using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCurrent : MonoBehaviour
{
    //animation slime
    const string PLAYER_IDLE = "Slime_Idle_Animation";
    const string PLAYER_MOVE = "Slime_Move";
    const string PLAYER_JUMP_START_UP = "Slime_Jump_Start_Up";
    const string PLAYER_HOLD_JUMP = "Slime_Hold_Jump";
    const string PLAYER_HOLD_JUMP_GREEN = "Slime_Hold_Jump_Green";
    const string PLAYER_HOLD_JUMP_RED = "Slime_Hold_Jump_Red";
    const string PLAYER_JUMP_UP = "Slime_Jump_Up";
    const string PLAYER_JUMP_TO_FALL = "Slime_Jump_To_Fall";
    const string PLAYER_JUMP_DOWN = "Slime_Jump_Down";
    const string PLAYER_JUMP_LAND = "Slime_Jump_Land";
    const string PLAYER_HURT = "Slime_Hurt";
    const string PLAYER_DEATH = "Slime_Death";

    //cache data
    //Level 1
    const string CHECK_POINT_LEVEL_1 = "Check_Point_Level_1";
    const string HEART_LEVEL_1 = "Heart_Level_1";

    //______________________________________________________________________

    public string GetPlayerIdle ()
    {
        return PLAYER_IDLE;
    }

    public string GetPlayerMove()
    {
        return PLAYER_MOVE;
    }

    public string GetPlayerJumpStartUp()
    {
        return PLAYER_JUMP_START_UP;
    }

    public string GetPlayerHoldJump()
    {
        return PLAYER_HOLD_JUMP;
    }

    public string GetPlayerHoldJumpGreen()
    {
        return PLAYER_HOLD_JUMP_GREEN;
    }

    public string GetPlayerHoldJumpRed()
    {
        return PLAYER_HOLD_JUMP_RED;
    }

    public string GetPlayerJumpUp()
    {
        return PLAYER_JUMP_UP;
    }

    public string GetPlayerJumpToFall()
    {
        return PLAYER_JUMP_TO_FALL;
    }

    public string GetPlayerJumpDown()
    {
        return PLAYER_JUMP_DOWN;
    }

    public string GetPlayerJumpLand()
    {
        return PLAYER_JUMP_LAND;
    }

    public string GetPlayerHurt()
    {
        return PLAYER_HURT;
    }

    public string GetPlayerDeath()
    {
        return PLAYER_DEATH;
    }

    public string GetCheckPointLevel1()
    {
        return CHECK_POINT_LEVEL_1;
    }

    public string GetHeartLevel1()
    {
        return HEART_LEVEL_1;
    }
}
