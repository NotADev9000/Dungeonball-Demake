using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackOnCollision)), RequireComponent(typeof(Launcher))]
public class Ammo : MonoBehaviour, IHaveATeam, IAmThrowable
{
    [field: SerializeField] public Team Team { get; set; }

    private AttackOnCollision _attackOnCollision;
    [Header("Attacking")]
    [Space(5)]
    [SerializeField] private AttackSpeedValidator _attackSpeedValidator;

    [Header("Attack attempted Actions")]
    [Space(5)]
    [SerializeField] private FlashMaterialsAction _flashOnAttackAttempt;
    [SerializeField] private SpeedScaleEaseAction _scaleOnAttackAttempt;

    private Launcher _launcher;

    private void Awake()
    {
        _attackOnCollision = GetComponent<AttackOnCollision>();
        _launcher = GetComponent<Launcher>();

        // ATTACKING
        List<IAmActionable> attackAttemptedActions = new() { _flashOnAttackAttempt, _scaleOnAttackAttempt };
        AttackHandler attackHandler = new(_attackSpeedValidator, true, attackAttemptedActions);
        _attackOnCollision.Init(attackHandler);
    }

    public void Throw(Vector3 direction)
    {
        _launcher.Launch(direction);
    }
}
