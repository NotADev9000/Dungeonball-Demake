using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackOnCollision))]
public class HoppingAttackerEnemy : MonoBehaviour, IHaveATeam, IReactToAttacks
{
    [field: SerializeField] public Teams Team { get; set; }

    private Damageable _damageable;

    [Header("Health")]
    [Space(5)]
    [SerializeField] private HealthData_SO _healthData;
    [Space(10)]

    [Header("Damage Effects")]
    [Space(5)]
    [SerializeField] private FlashMaterialsAction _flashOnDamage;
    [Space(10)]

    [Header("Death Effects")]
    [Space(5)]
    [SerializeField] private DestroyAction _destroyOnDeathDelay;
    [SerializeField] private FlashMaterialsAction _flashOnDeath;
    [Space(10)]

    private AttackOnCollision _attackOnCollision;

    [Header("Attacking")]
    [Space(5)]
    [SerializeField] private AttackAngleValidator _attackAngleValidator;
    private ActivateAttackOnJump _activateAttackOnJump;

    private void Awake()
    {
        List<IAmActionable> damageNoDeathActions = new() { _flashOnDamage };
        List<IAmActionable> deathActions = new() { _flashOnDeath, _destroyOnDeathDelay };
        _damageable = new Damageable(this, _healthData, damageNoDeathActions, null, deathActions);

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
