using UnityEngine;

public class TimedPathCalculationStrategy : PathCalculationStrategy
{
    [SerializeField] private float _timeBetweenPathRecalc = 0.2f;

    private float _pathRecalculationTimer;

    public override void UpdatePathRecalculation()
    {
        _pathRecalculationTimer += Time.deltaTime;
        if (_pathRecalculationTimer >= _timeBetweenPathRecalc)
        {
            Reset();
            _pathfinder.CalculatePathToTarget();
        }
    }

    public override void Reset()
    {
        _pathRecalculationTimer = 0f;
    }
}
