using System;
using UnityEngine;

public class Player_Combat : MonoBehaviour, IHaveATeam, IReactToAttacks
{
    [field: SerializeField] public Teams Team { get; set; }

    private Damageable _damageable;

    [Header("Health")]
    [Space(5)]
    [SerializeField] private HealthData_SO _healthData;
    // [Space(10)]

    // [Header("Death Effects")]
    // [Space(5)]

    public event Action OnDeath;
    private Action HandlePlayerDeath => () => OnDeath?.Invoke();

    private void Awake()
    {
        // List<IAmActionable> deathActions = new() { };
        _damageable = new Damageable(_healthData, null, null, null);
    }

    private void OnEnable()
    {
        _damageable.OnDeath += () => HandlePlayerDeath();
    }

    private void OnDisable()
    {
        _damageable.OnDeath -= () => HandlePlayerDeath();
    }

    public void OnAttackReceived(Teams attackerTeam)
    {
        _damageable.ProcessIncomingAttack(Team, attackerTeam);
    }
}
