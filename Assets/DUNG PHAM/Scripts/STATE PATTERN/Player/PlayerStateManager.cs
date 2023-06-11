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
    PlayerAttackManager playerAttack;
    PlayerDash playerDash;
    PlayerWallLedgeGrabAndClimb playerLedge;
    PlayerWallSlideAndJump playerWall;
    [HideInInspector] public PlayerInteractTile playerInteract;
    [HideInInspector] public Rigidbody2D rigid;
    void GetObjectComponents()
    {
        playerDatabase = GetComponent<PlayerDatabase>();
        playerController = GetComponent<PlayerController>();
        playerCollision = GetComponent<PlayerCollisionDetector>();
        inputController = GetComponent<InputControllerNew>();
        playerAnimation = GetComponent<PlayerAnimation>();
        soundEffect = GetComponentInChildren<SoundEffect>();

        playerAttack = GetComponent<PlayerAttackManager>();
        playerDash = GetComponent<PlayerDash>();
        playerLedge = GetComponent<PlayerWallLedgeGrabAndClimb>();
        playerWall = GetComponent<PlayerWallSlideAndJump>();
        playerInteract = GetComponent<PlayerInteractTile>();

        rigid = GetComponent<Rigidbody2D>();
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
    public PlayerWallEdgeState wallLedgeState;
    public PlayerWallClimbState wallClimbState;
    public PlayerHurtState hurtState;
    public PlayerDieState dieState;
    public PlayerLadderClimbState ladderState;

    void StateDeclaration()
    {
        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        jumpState = new PlayerJumpState(this);
        runState = new PlayerRunState(this);
        fallState = new PlayerFallState(this);
        onAirState = new PlayerOnAirState(this);
        dashState = new PlayerDashState(this, playerDash);
        attackState = new PlayerAttackState(this, playerAttack);
        attackState1 = new PlayerAttackState1(this, playerAttack);
        attackState2 = new PlayerAttackState2(this, playerAttack);
        airAttackState = new PlayerAirAttackState(this, playerAttack);
        crouchState = new PlayerCrouchState(this);
        crouchMoveState = new PlayerCrouchMoveState(this);
        wallSlideState = new PlayerWallSlideState(this);
        wallJumpState = new PlayerWallJumpState(this);
        wallLedgeState = new PlayerWallEdgeState(this);
        wallClimbState = new PlayerWallClimbState(this);
        hurtState = new PlayerHurtState(this, playerAttack);
        dieState = new PlayerDieState(this);
        ladderState = new PlayerLadderClimbState(this, playerInteract);
    }

    void Awake()
    {
        GetObjectComponents();
        StateDeclaration();
    }
    void Start()
    {
        currentState = idleState;

        currentState.EnterState();
    }

    #endregion
}
