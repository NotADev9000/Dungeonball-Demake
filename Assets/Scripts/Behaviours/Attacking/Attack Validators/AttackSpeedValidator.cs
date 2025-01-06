using System;
using UnityEngine;

// TODO: Check rigidbody is above certain speed
[Serializable]
public class AttackSpeedValidator : IValidateAttack
{
    public bool IsAttackValid(GameObject target)
    {
        return true;
    }
}
