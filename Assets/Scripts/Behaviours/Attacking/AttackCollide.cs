using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollide : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackAngle = 60f;

    private ICollide _collideReactor;

    public bool IsAttackActive { get; set; }

    private void Awake()
    {
        _collideReactor = GetComponent<ICollide>();
    }

    public void OnAttack(GameObject other)
    {
        if (!IsAttackActive) return;

        if (TryGetHittable(other, out ICollide hittable) && IsTargetWithinAttackAngle(other.transform.position))
        {
            AttackHittable(hittable);
        }
    }

    private bool TryGetHittable(GameObject other, out ICollide hittable)
    {
        hittable = other.GetComponent<ICollide>();
        return hittable != null;
    }

    private void AttackHittable(ICollide hittable)
    {
        hittable.GetHit(_collideReactor.Team);
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
