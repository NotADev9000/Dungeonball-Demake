/// <summary>
/// Executes a start action, and then executes delayed actions when the start action ends.
/// e.g. Flash material action is executed, once finished, execute change material action.
/// </summary>
public class CompoundAction : IAmActionable
{
    // start action must be an endable action, so that we can know when it has finished.
    private IAmEndableAction _startAction;
    private IAmActionable[] _delayedActions;

    public CompoundAction(IAmEndableAction startAction, IAmActionable[] delayedActions)
    {
        _startAction = startAction;
        _delayedActions = delayedActions;
    }

    public CompoundAction(IAmEndableAction startAction, IAmActionable delayedAction)
    {
        _startAction = startAction;
        _delayedActions = new IAmActionable[] { delayedAction };
    }

    public void Execute()
    {
        _startAction.OnActionEnded += ExecuteDelayed;
        _startAction.Execute();
    }

    private void ExecuteDelayed()
    {
        _startAction.OnActionEnded -= ExecuteDelayed;
        foreach (IAmActionable action in _delayedActions)
        {
            action.Execute();
        }
    }
}