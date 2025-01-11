using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler
{
    private IValidateAttack _attackValidator;

    public bool IsAttackActive { get; set; }

    public AttackHandler(IValidateAttack validator = null, bool isAttackActive = false)
    {
        _attackValidator = validator ?? new NullAttackValidator();
        IsAttackActive = isAttackActive;
    }

    public void OnAttack(GameObject target, Team attackerTeam)
    {
        if (!IsAttackActive) return;

        if (target.TryGetComponent(out IReactToAttacks attackReactor) && _attackValidator.IsAttackValid(target))
        {
            attackReactor.OnAttackReceived(attackerTeam);
        }
    }
}
