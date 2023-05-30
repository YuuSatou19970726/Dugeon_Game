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

    //animation dragon warrior
    const string DRAGON_WARRIOR_IDLE = "Dragon_Warrior_Idle_Animation";
    const string DRAGON_WARRIOR_MOVE = "Dragon_Warrior_Move_Animation";
    const string DRAGON_WARRIOR_DIE = "Dragon_Warrior_Die_Animation";
    const string DRAGON_WARRIOR_ATTACK = "Dragon_Warrior_Attack_Animation";

    //animation fire ball
    const string FIRE_BALL_ATTACK = "Fire_Ball_Animation";
    const string FIRE_BALL_EXPLOSION = "Fire_Ball_Explosion_Animation";

    //animation iblast
    const string IBLAST_ATTACK = "Iblast_Attack_Animation";
    const string IBLAST_EXPLOSION = "Iblast_Explosion_Animation";

    //cache data
    //Level 1
    const string CHECK_POINT_LEVEL_1 = "Check_Point_Level_1";
    const string HEART_LEVEL_1 = "Heart_Level_1";
    const string SCORE = "Score";

    //______________________________________________________________________

    //Slime Animation
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

    //Dragon Warrior Animation
    public string GetDragonWarriorIdle()
    {
        return DRAGON_WARRIOR_IDLE;
    }

    public string GetDragonWarriorMove()
    {
        return DRAGON_WARRIOR_MOVE;
    }

    public string GetDragonWarriorDie()
    {
        return DRAGON_WARRIOR_DIE;
    }

    public string GetDragonWarriorAttack()
    {
        return DRAGON_WARRIOR_ATTACK;
    }

    //animation fire ball
    public string GetFireBallAttack()
    {
        return FIRE_BALL_ATTACK;
    }

    public string GetFireBallExplosion()
    {
        return FIRE_BALL_EXPLOSION;
    }

    //animation iblast
    public string GetIblastAttack()
    {
        return IBLAST_ATTACK;
    }

    public string GetIblastExplosion()
    {
        return IBLAST_EXPLOSION;
    }

    public string GetCheckPointLevel1()
    {
        return CHECK_POINT_LEVEL_1;
    }

    public string GetHeartLevel1()
    {
        return HEART_LEVEL_1;
    }

    public string GetScore()
    {
        return SCORE;
    }
}
