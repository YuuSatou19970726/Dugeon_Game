
public interface IState
{
    void EnterState(PlayerStateManager player);
    void UpdateState(PlayerStateManager player);
    void FixedUpdateState(PlayerStateManager player);
    void ExitState(PlayerStateManager player);
}
