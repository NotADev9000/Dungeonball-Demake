using UnityEngine;

/// <summary>
/// Strategies to determine when to recalculate path.
/// </summary>
public abstract class PathCalculationStrategy : MonoBehaviour
{
    protected Pathfinder _pathfinder;

    public void SetPathfinder(Pathfinder pathfinder)
    {
        _pathfinder = pathfinder;
    }

    public abstract void UpdatePathRecalculation();
    public abstract void Reset();
}
