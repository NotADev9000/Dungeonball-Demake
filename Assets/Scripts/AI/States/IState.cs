public interface IState
{
    void InitState();
    void Enter();
    void Update();
    void FixedUpdate();
    void Exit();
}
