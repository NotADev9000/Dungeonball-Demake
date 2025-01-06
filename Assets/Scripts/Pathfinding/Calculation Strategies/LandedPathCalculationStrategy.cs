using UnityEngine;

/// <summary>
/// Recalculates the path the frame the object lands
[RequireComponent(typeof(GroundSensor))]
public class LandedPathCalculationStrategy : PathCalculationStrategy
{
    private GroundSensor _groundSensor;

    private bool _hasCalcedLastFrame;

    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
    }

    public override void UpdatePathRecalculation()
    {
        if (_hasCalcedLastFrame)
        {
            _hasCalcedLastFrame = false;
            return;
        }

        if (_groundSensor.LandedThisFrame)
        {
            Reset();
            _pathfinder.CalculatePathToTarget();
            // Debug.Log("Recalculating path");
        }
    }

    public override void Reset()
    {
        _hasCalcedLastFrame = true;
    }
}