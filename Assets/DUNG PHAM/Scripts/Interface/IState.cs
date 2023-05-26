
public interface IState
{
    void EnterState();
    void UpdateState();
    void FixedUpdateState();
    void ExitState();
}
