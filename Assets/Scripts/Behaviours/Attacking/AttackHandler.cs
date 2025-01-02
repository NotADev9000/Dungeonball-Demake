using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler
{
    private IValidateAttack _attackValidator;

    public bool IsAttackActive { get; set; }

    public AttackHandler(IValidateAttack attackValidator)
    {
        _attackValidator = attackValidator;
    }

    public void OnAttack(GameObject target, Team attackerTeam)
    {
        if (!IsAttackActive) return;

        if (target.TryGetComponent(out ICollide hittable) && _attackValidator.IsAttackValid(target))
        {
            hittable.GetHit(attackerTeam);
        }
    }
}
