using System.Collections.Generic;

public class Damageable
{
    private IHaveATeam _teamOwner;
    private DamageHandler _damageHandler;
    private HealthComponent _healthComponent;

    private List<IActions> _onDamageNoDeathActions;
    private List<IActions> _onDeathActions;
    private List<IActions> _onTeamDifferentActions;

    public Damageable(
        IHaveATeam teamOwner,
        HealthData_SO healthData_SO,
        List<IActions> onDamageNoDeathActions = null,
        List<IActions> onDeathActions = null,
        List<IActions> onTeamDifferentActions = null
    )
    {
        _teamOwner = teamOwner;
        _healthComponent = new HealthComponent(healthData_SO);
        _damageHandler = new DamageHandler(_healthComponent);
        _onDamageNoDeathActions = onDamageNoDeathActions ?? new List<IActions>();
        _onDeathActions = onDeathActions ?? new List<IActions>();
        _onTeamDifferentActions = onTeamDifferentActions ?? new List<IActions>();
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
                    // damage no death actions
                    OnDamageNoDeath();
                }
            }
        }
    }

    private void OnDamageNoDeath()
    {
        ExecuteActions(_onDamageNoDeathActions);
    }

    private void OnDeath()
    {
        ExecuteActions(_onDeathActions);
    }

    private void OnTeamDifference()
    {
        ExecuteActions(_onTeamDifferentActions);
    }

    private void ExecuteActions(List<IActions> actions)
    {
        foreach (IActions action in actions)
        {
            action.Execute();
        }
    }
}
