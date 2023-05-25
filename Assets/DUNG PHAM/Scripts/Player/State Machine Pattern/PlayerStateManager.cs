using UnityEngine;

public class PlayerStateManager : MonoBehaviour
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

    public IState currentState;
    public IState previousState;
    bool inTransition = false;

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
        idleState = new PlayerIdleState();
        walkState = new PlayerWalkState();
        jumpState = new PlayerJumpState(playerJump);
        runState = new PlayerRunState();
        fallState = new PlayerFallState();
        onAirState = new PlayerOnAirState();
        dashState = new PlayerDashState(playerDash);
        attackState = new PlayerAttackState();
        attackState1 = new PlayerAttackState1();
        attackState2 = new PlayerAttackState2();
        airAttackState = new PlayerAirAttackState();
        crouchState = new PlayerCrouchState();
        crouchMoveState = new PlayerCrouchMoveState();
        wallSlideState = new PlayerWallSlideState(playerWall);
        wallJumpState = new PlayerWallJumpState(playerWall);
        wallEdge = new PlayerWallEdgeState(playerLedge);
        wallClimb = new PlayerWallClimbState(playerLedge);
        hurtState = new PlayerHurtState();
        dieState = new PlayerDieState();
    }

    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    public void Update()
    {
        if (currentState != null && !inTransition)
            currentState.UpdateState(this);

        Debug.Log(currentState);
    }

    public void FixedUpdate()
    {
        if (currentState != null && !inTransition)
            currentState.FixedUpdateState(this);
    }

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    public void SwitchState(IState newState)
    {
        if (currentState == newState || inTransition)
            return;

        ChangeStateRoutine(newState);
    }

    void ChangeStateRoutine(IState newState)
    {
        inTransition = true;

        if (currentState != null)
            currentState.ExitState(this);

        if (previousState != null)
            previousState = currentState;

        currentState = newState;

        if (currentState != null)
            currentState.EnterState(this);

        inTransition = false;
    }

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    public void RevertState()
    {
        if (previousState != null)
            SwitchState(previousState);
    }
    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/

    #endregion
}
