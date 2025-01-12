using UnityEngine;

/// <summary>
/// Actions that are executed on collision.
/// </summary>
public interface IAmCollisionAction : IAmActionable
{
    public void Execute(Collision other);
}