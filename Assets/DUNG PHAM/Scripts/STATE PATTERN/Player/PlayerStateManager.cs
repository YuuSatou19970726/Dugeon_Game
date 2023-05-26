using UnityEngine;

public class PlayerStateManager : AStateMachine
{
    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/

    #region MONO BEHAVIOUR 
    [HideInInspector] public PlayerDatabase playerDatabase;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerCollisionDetector playerCollision;
    [HideInInspector] public InputControllerNew inputController;
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public SoundEffect soundEffect;
    [HideInInspector] public PlayerAttackManager playerAttack;
    PlayerJump playerJump;
    PlayerDash playerDash;
    PlayerWallLedgeGrabAndClimb playerLedge;
    PlayerWallSlideAndJump playerWall;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttackManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        soundEffect = GetComponentInChildren<SoundEffect>();
        playerJump = GetComponent<PlayerJump>();
        playerDash = GetComponent<PlayerDash>();
        playerLedge = GetComponent<PlayerWallLedgeGrabAndClimb>();
        playerWall = GetComponent<PlayerWallSlideAndJump>();
        playerCollision = GetComponent<PlayerCollisionDetector>();

        StateDeclaration();
    }
    #endregion

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    #region  ALL STATE

    public PlayerIdleState idleState;
    public PlayerWalkState walkState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerOnAirState onAirState;
    public PlayerFallState fallState;
    public PlayerDashState dashState;
    public PlayerAttackState attackState;
    public PlayerAttackState1 attackState1;
    public PlayerAttackState2 attackState2;
    public PlayerAirAttackState airAttackState;
    public PlayerCrouchState crouchState;
    public PlayerCrouchMoveState crouchMoveState;
    public PlayerWallSlideState wallSlideState;
    public PlayerWallJumpState wallJumpState;
    public PlayerWallEdgeState wallEdge;
    public PlayerWallClimbState wallClimb;
    public PlayerHurtState hurtState;
    public PlayerDieState dieState;


    void StateDeclaration()
    {
        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        jumpState = new PlayerJumpState(this, playerJump);
        runState = new PlayerRunState(this);
        fallState = new PlayerFallState(this);
        onAirState = new PlayerOnAirState(this);
        dashState = new PlayerDashState(this, playerDash);
        attackState = new PlayerAttackState(this);
        attackState1 = new PlayerAttackState1(this);
        attackState2 = new PlayerAttackState2(this);
        airAttackState = new PlayerAirAttackState(this);
        crouchState = new PlayerCrouchState(this);
        crouchMoveState = new PlayerCrouchMoveState(this);
        wallSlideState = new PlayerWallSlideState(this, playerWall);
        wallJumpState = new PlayerWallJumpState(this, playerWall);
        wallEdge = new PlayerWallEdgeState(this, playerLedge);
        wallClimb = new PlayerWallClimbState(this, playerLedge);
        hurtState = new PlayerHurtState(this);
        dieState = new PlayerDieState(this);
    }

    void Start()
    {
        currentState = idleState;

        currentState.EnterState();
    }

    #endregion
}
