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

    [Header("On Collision Actions")]
    [Space(5)]
    [SerializeField] private FlashMaterialsAction _flashOnCollision;
    [SerializeField] private SpeedScaleEaseAction _scaleOnCollision;
    [SerializeField] private InstantiateParticleAction _instantiateParticleOnCollision;

    private Launcher _launcher;

    private void Awake()
    {
        _attackOnCollision = GetComponent<AttackOnCollision>();
        _launcher = GetComponent<Launcher>();

        // ATTACKING
        List<IAmActionable> collidedActions = new() { _flashOnCollision, _scaleOnCollision, _instantiateParticleOnCollision };
        List<IAmCollisionAction> collidedCollisionActions = new() { _instantiateParticleOnCollision };
        AttackHandler attackHandler = new(_attackSpeedValidator, true);
        _attackOnCollision.Init(attackHandler, collidedActions, collidedCollisionActions);
    }

    public void Throw(Vector3 direction, Collider throwerCollider = null)
    {
        _launcher.Launch(direction, throwerCollider);
    }
}
