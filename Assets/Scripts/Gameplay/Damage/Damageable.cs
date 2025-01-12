using System;
using System.Collections.Generic;

public class Damageable
{
    private DamageHandler _damageHandler;
    private Health _healthComponent;
    public Health HealthComponent => _healthComponent;
    private DeathHandler _deathHandler;

    private List<IAmActionable> _onDamageNoDeathActions;
    private List<IAmActionable> _onTeamDifferentActions;

    public event Action OnDeath;

    public Damageable(
        HealthData_SO healthData_SO,
        List<IAmActionable> onDamageNoDeathActions = null,
        List<IAmActionable> onTeamDifferentActions = null,
        List<IAmActionable> onDeathActions = null
    )
    {
        _healthComponent = new Health(healthData_SO);
        _damageHandler = new DamageHandler(_healthComponent);
        _onDamageNoDeathActions = onDamageNoDeathActions ?? new List<IAmActionable>();
        _onTeamDifferentActions = onTeamDifferentActions ?? new List<IAmActionable>();
        _deathHandler = new DeathHandler(onDeathActions);
    }

    public void Init()
    {
        _healthComponent.Init();
    }

    public void ProcessIncomingAttack(Team myTeam, Team attackerTeam, int damage)
    {
        if (_healthComponent.IsDead) return;

        if (TeamUtils.IsOnDifferentTeam(myTeam, attackerTeam))
        {
            OnTeamDifference();
            if (_damageHandler.TryDamage(damage))
            {
                if (_healthComponent.IsDead)
                {
                    ProcessDeath();
                }
                else
                {
                    // damage but not dead actions
                    OnDamageNoDeath();
                }
            }
        }
    }

    private void OnDamageNoDeath()
    {
        ExecuteActions(_onDamageNoDeathActions);
    }

    private void OnTeamDifference()
    {
        ExecuteActions(_onTeamDifferentActions);
    }

    private void ExecuteActions(List<IAmActionable> actions)
    {
        foreach (IAmActionable action in actions)
        {
            action.Execute();
        }
    }

    private void ProcessDeath()
    {
        OnDeath?.Invoke();
        _deathHandler.OnDeath();
    }
}
