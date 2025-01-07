using System.Collections.Generic;

public class Damageable
{
    private IHaveATeam _teamOwner;
    private DamageHandler _damageHandler;
    private HealthComponent _healthComponent;
    private DeathHandler _deathHandler;

    private List<IAmActionable> _onDamageNoDeathActions;
    private List<IAmActionable> _onTeamDifferentActions;

    public Damageable(
        IHaveATeam teamOwner,
        HealthData_SO healthData_SO,
        List<IAmActionable> onDamageNoDeathActions = null,
        List<IAmActionable> onTeamDifferentActions = null,
        List<IAmActionable> onDeathActions = null
    )
    {
        _teamOwner = teamOwner;
        _healthComponent = new HealthComponent(healthData_SO);
        _damageHandler = new DamageHandler(_healthComponent);
        _onDamageNoDeathActions = onDamageNoDeathActions ?? new List<IAmActionable>();
        _onTeamDifferentActions = onTeamDifferentActions ?? new List<IAmActionable>();
        _deathHandler = new DeathHandler(onDeathActions);
    }

    public void ProcessIncomingAttack(Teams attackerTeam)
    {
        if (TeamUtils.IsOnDifferentTeam(_teamOwner.Team, attackerTeam))
        {
            OnTeamDifference();
            if (_damageHandler.TryDamage(10))
            {
                if (_healthComponent.IsDead)
                {
                    // death actions
                    OnDeath();
                }
                else
                {
                    // damage but not dead  actions
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

    private void OnDeath()
    {
        _deathHandler.OnDeath();
    }
}
