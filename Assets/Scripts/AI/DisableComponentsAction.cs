using UnityEngine;

public class DisableComponentsAction : IAmActionable
{
    private MonoBehaviour[] _componentsToDisable;

    public DisableComponentsAction(MonoBehaviour[] components)
    {
        _componentsToDisable = components;
    }

    public DisableComponentsAction(MonoBehaviour component)
    {
        _componentsToDisable = new[] { component };
    }

    public void Execute()
    {
        foreach (MonoBehaviour component in _componentsToDisable)
        {
            component.enabled = false;
        }
    }

}