using System;
using UnityEngine;

public class ActivateAttackOnJump : IDisposable
{
    private AttackHandler _attackHandler;
    private MovementJump _movementJump;
    private GroundSensor _groundSensor;

    public ActivateAttackOnJump(AttackHandler attackHandler, GameObject gameObject)
    {
        _attackHandler = attackHandler;
        _movementJump = gameObject.GetComponent<MovementJump>();
        _groundSensor = gameObject.GetComponent<GroundSensor>();

        if (_movementJump != null && _groundSensor != null)
        {
            _movementJump.OnJump += EnableAttack;
            _groundSensor.OnLandedThisFrame += DisableAttack;
        }
        else
        {
            throw new NullReferenceException("Missing move and groundSensor components");
        }
    }

    public void Dispose()
    {
        if (_movementJump != null && _groundSensor != null)
        {
            _movementJump.OnJump -= EnableAttack;
            _groundSensor.OnLandedThisFrame -= DisableAttack;
        }
    }

    private void EnableAttack() => _attackHandler.IsAttackActive = true;
    private void DisableAttack() => _attackHandler.IsAttackActive = false;
}
