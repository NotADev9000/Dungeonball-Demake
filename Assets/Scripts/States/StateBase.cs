public abstract class StateBase : IState
{
    public virtual void InitState() { }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FixedUpdate() { }
    public virtual void Update() { }
}
