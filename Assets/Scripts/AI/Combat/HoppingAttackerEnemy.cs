using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackOnCollision)), RequireComponent(typeof(ChaserAI))]
public class HoppingAttackerEnemy : MonoBehaviour, IHaveATeam, IReactToAttacks
{
    [field: SerializeField] public Team Team { get; set; }

    private ChaserAI _aiStateMachine;
    private Damageable _damageable;

    [Header("Health")]
    [Space(5)]
    [SerializeField] private HealthData_SO _healthData;
    [Space(10)]

    [Header("Damage Effects")]
    [Space(5)]
    [SerializeField] private FlashMaterialsAction _flashOnDamage;
    [SerializeField] private PlaySoundAction _damageSoundAction;
    [Space(10)]

    [Header("Death Effects")]
    [Space(5)]
    [SerializeField] private DestroyAction _destroyOnDeathDelay;
    [SerializeField] private ChangeMaterialsAction _changeMatsOnDeath;
    [SerializeField] private BlinkObjectAction _blinkOnDeath;
    [SerializeField] private ChangeLayerAction _changeLayerOnDeath;
    [Space(10)]

    private AttackOnCollision _attackOnCollision;
    [Header("Attacking")]
    [Space(5)]
    [SerializeField] private AttackAngleValidator _attackAngleValidator;
    private ActivateAttackOnJump _activateAttackOnJump;

    private void Awake()
    {
        _aiStateMachine = GetComponent<ChaserAI>();
        _attackOnCollision = GetComponent<AttackOnCollision>();

        // DAMAGE
        List<IAmActionable> damageNoDeathActions = new() { _flashOnDamage, _damageSoundAction };
        // compound action run in sequence e.g. flash object first, then run remaining actions
        CompoundAction actionsAfterDeathFlash = new(_flashOnDamage, new IAmActionable[] { _changeMatsOnDeath, _blinkOnDeath, _changeLayerOnDeath });
        List<IAmActionable> deathActions = new() { actionsAfterDeathFlash, _destroyOnDeathDelay, new DisableComponentsAction(_aiStateMachine), _damageSoundAction };
        _damageable = new Damageable(_healthData, damageNoDeathActions, null, deathActions);

        // ATTACKING
        AttackHandler attackHandler = new(_attackAngleValidator);
        _attackOnCollision.Init(attackHandler);
        _activateAttackOnJump = new ActivateAttackOnJump(attackHandler, gameObject);
    }

    private void Start()
    {
        _damageable.Init();
    }

    private void OnDisable()
    {
        _activateAttackOnJump.Dispose();
    }

    public void OnAttackReceived(Team attackerTeam, int damage)
    {
        _damageable.ProcessIncomingAttack(Team, attackerTeam, damage);
    }
}
