using System;

public class InvokeEventAction : IAmActionable
{
    private Action _action;

    public InvokeEventAction(Action action)
    {
        _action = action;
    }

    public void Execute()
    {
        _action?.Invoke();
    }
}