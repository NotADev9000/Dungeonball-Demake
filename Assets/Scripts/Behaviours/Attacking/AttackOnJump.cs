using System;
using UnityEngine;

public class AttackOnJump : IDisposable
{
    private AttackHandler _attackHandler;
    private MovementJump _movementJump;
    private GroundSensor _groundSensor;

    public AttackOnJump(AttackHandler attackHandler, GameObject gameObject)
    {
        _attackHandler = attackHandler;
        _movementJump = gameObject.GetComponent<MovementJump>();
        _groundSensor = gameObject.GetComponent<GroundSensor>();

        _movementJump.OnJump += EnableAttack;
        _groundSensor.OnLandedThisFrame += DisableAttack;
    }

    public void Dispose()
    {
        _movementJump.OnJump -= EnableAttack;
        _groundSensor.OnLandedThisFrame -= DisableAttack;
    }

    private void EnableAttack() => _attackHandler.IsAttackActive = true;
    private void DisableAttack() => _attackHandler.IsAttackActive = false;
}
