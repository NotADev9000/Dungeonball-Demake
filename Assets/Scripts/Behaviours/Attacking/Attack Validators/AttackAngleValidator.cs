using System;
using UnityEngine;

[Serializable]
public class AttackAngleValidator : IValidateAttack
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackAngle = 60f;

    public bool IsAttackValid(GameObject target)
    {
        return IsTargetWithinAttackAngle(target.transform.position);
    }

    private bool IsTargetWithinAttackAngle(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - _attackPoint.position).normalized;
        targetDirection.y = 0f;

        Vector3 attackForward = _attackPoint.forward;
        attackForward.y = 0f;

        float angle = Vector3.Angle(attackForward, targetDirection);
        return angle <= _attackAngle / 2;
    }
}