using System;
using UnityEngine;

[Serializable]
public class StateChase : StateBase
{
    [SerializeField] private InputController_AI _input;
    [SerializeField] private PathCalculationStrategy _pathCalculationStrategy;

    private Pathfinder _pathfinder;
    // public Transform Target { get; set; }

    public override void InitState()
    {
        CheckForMissingComponents();
        _pathfinder = _input.Pathfinder;
        _pathCalculationStrategy.SetPathfinder(_pathfinder);
        _input.enabled = false;
    }

    public override void Enter()
    {
        _input.enabled = true;
        _pathfinder = _input.Pathfinder;
        _pathfinder.CalculatePathToTarget();
        _pathCalculationStrategy.SetPathfinder(_pathfinder);
        _pathCalculationStrategy.Reset();
    }

    public override void Exit()
    {
        _input.enabled = false;
    }

    public void SetTarget(Transform target)
    {
        _pathfinder.SetTarget(target);
    }

    public override void Update()
    {
        _pathCalculationStrategy.UpdatePathRecalculation();
    }

    private void CheckForMissingComponents()
    {
        if (_input == null)
            throw new MissingComponentException("InputController_AI component is missing");
        if (_pathCalculationStrategy == null)
            throw new MissingComponentException("PathCalculationStrategy component is missing");
    }
}
