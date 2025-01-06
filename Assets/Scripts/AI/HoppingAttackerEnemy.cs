using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackOnCollision))]
public class HoppingAttackerEnemy : MonoBehaviour, IHaveATeam, IReactToAttacks//, IReactToDeath
{
    [field: SerializeField] public Teams Team { get; set; }

    private Damageable _damageable;

    [Header("Health")]
    [Space(5)]
    [SerializeField] private HealthData_SO _healthData;
    [Space(10)]

    [Header("Visual Effects")]
    [Space(5)]
    [SerializeField] private FlashMaterialsAction _flashOnDamage;
    [Space(10)]

    private AttackOnCollision _attackOnCollision;

    [Header("Attacking")]
    [Space(5)]
    [SerializeField] private AttackAngleValidator _attackAngleValidator;
    private ActivateAttackOnJump _activateAttackOnJump;

    private void Awake()
    {
        List<IActions> damageNoDeathActions = new() { _flashOnDamage };
        _damageable = new Damageable(this, _healthData, damageNoDeathActions);

        _attackOnCollision = GetComponent<AttackOnCollision>();

        AttackHandler attackHandler = new(_attackAngleValidator);

        _attackOnCollision.Init(attackHandler);
        _activateAttackOnJump = new ActivateAttackOnJump(attackHandler, gameObject);
    }

    private void OnDisable()
    {
        _activateAttackOnJump.Dispose();
    }

    public void OnAttackReceived(Teams attackerTeam)
    {
        _damageable.ProcessIncomingAttack(attackerTeam);
    }
}
