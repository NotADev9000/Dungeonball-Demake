using System.Collections.Generic;

public class DeathHandler
{
    private List<IAmActionable> _onDeathActions = null;

    public DeathHandler(List<IAmActionable> onDeathActions = null)
    {
        this._onDeathActions = onDeathActions ?? new List<IAmActionable>();
    }

    public void OnDeath()
    {
        ExecuteActions(_onDeathActions);
    }

    private void ExecuteActions(List<IAmActionable> actions)
    {
        foreach (IAmActionable action in actions)
        {
            action.Execute();
        }
    }
}