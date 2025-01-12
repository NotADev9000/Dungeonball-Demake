using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour, IHaveATeam, IReactToAttacks
{
    [field: SerializeField] public Team Team { get; set; }

    private Damageable _damageable;
    public Health HealthComponent => _damageable.HealthComponent;

    [Header("Health")]
    [Space(5)]
    [SerializeField] private HealthData_SO _healthData;
    [Space(10)]

    [Header("Damage Effects")]
    [Space(5)]
    [SerializeField] private PlaySoundAction _damageSoundAction;
    [Space(10)]

    [Header("Death Effects")]
    [Space(5)]
    [SerializeField] private PlaySoundAction _deathSoundAction;

    public event Action OnDeath;
    private Action HandlePlayerDeath => () => OnDeath?.Invoke();

    private void Awake()
    {
        List<IAmActionable> damageNoDeathActions = new() { _damageSoundAction };
        List<IAmActionable> deathActions = new() { _deathSoundAction };
        _damageable = new Damageable(_healthData, damageNoDeathActions, null, deathActions);
    }

    private void Start()
    {
        _damageable.Init();
    }

    private void OnEnable()
    {
        _damageable.OnDeath += () => HandlePlayerDeath();
    }

    private void OnDisable()
    {
        _damageable.OnDeath -= () => HandlePlayerDeath();
    }

    public void OnAttackReceived(Team attackerTeam, int damage)
    {
        _damageable.ProcessIncomingAttack(Team, attackerTeam, damage);
    }
}
