using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector] public PlayerMovementController playerMovementController;
    [HideInInspector] public InputControllerNew inputController;
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public PlayerAttackManager playerAttack;
    PlayerBaseState currentState;

    #region ALL STATE
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerRunState runState = new PlayerRunState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerOnAirState onAirState = new PlayerOnAirState();
    public PlayerFallState fallState = new PlayerFallState();
    public PlayerDashState dashState = new PlayerDashState();
    public PlayerAttackState attackState = new PlayerAttackState();
    public PlayerAttackState1 attackState1 = new PlayerAttackState1();
    public PlayerAttackState2 attackState2 = new PlayerAttackState2();
    public PlayerAirAttackState airAttackState = new PlayerAirAttackState();
    public PlayerCrouchState crouchState = new PlayerCrouchState();
    public PlayerCrouchMoveState crouchMoveState = new PlayerCrouchMoveState();
    public PlayerWallSlideState wallSlideState = new PlayerWallSlideState();
    public PlayerWallJumpState wallJumpState = new PlayerWallJumpState();
    public PlayerHurtState hurtState = new PlayerHurtState();
    public PlayerDieState dieState = new PlayerDieState();
    #endregion


    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerAttack = GetComponent<PlayerAttackManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputController = GetComponent<InputControllerNew>();
    }
    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);

        
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        if (state != currentState)
            currentState.ExitState(this);


        currentState = state;

        currentState.EnterState(this);

        // Debug.Log(currentState);
    }
}