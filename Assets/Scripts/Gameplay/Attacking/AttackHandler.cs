using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler
{
    private IValidateAttack _attackValidator;

    public bool IsAttackActive { get; set; }

    // actions that are not dependent on the attack being active
    public List<IAmActionable> _onAttackAttemptActions;
    // actions that are dependent on the attack being valid
    public List<IAmActionable> _onAttackedWhileActiveActions;

    public AttackHandler(IValidateAttack validator = null, bool isAttackActive = false, List<IAmActionable> onAttackAttemptActions = null, List<IAmActionable> onAttackedWhileActiveActions = null)
    {
        _attackValidator = validator ?? new NullAttackValidator();
        IsAttackActive = isAttackActive;
        _onAttackAttemptActions = onAttackAttemptActions ?? new List<IAmActionable>();
        _onAttackedWhileActiveActions = onAttackedWhileActiveActions ?? new List<IAmActionable>();
    }

    public void OnAttack(GameObject target, Team attackerTeam)
    {
        ExecuteActions(_onAttackAttemptActions);
        if (!IsAttackActive) return;

        ExecuteActions(_onAttackedWhileActiveActions);
        if (target.TryGetComponent(out IReactToAttacks attackReactor) && _attackValidator.IsAttackValid(target))
        {
            attackReactor.OnAttackReceived(attackerTeam);
        }
    }

    private void ExecuteActions(List<IAmActionable> actions)
    {
        foreach (IAmActionable action in actions)
        {
            action.Execute();
        }
    }
}
