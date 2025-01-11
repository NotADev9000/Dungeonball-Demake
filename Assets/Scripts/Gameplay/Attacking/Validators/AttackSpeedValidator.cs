using System;
using UnityEngine;

// TODO: Check rigidbody is above certain speed
[Serializable]
public class AttackSpeedValidator : IValidateAttack
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _minSpeed = 1f;

    public bool IsAttackValid(GameObject target)
    {
        return _rigidbody.velocity.magnitude >= _minSpeed;
    }
}
