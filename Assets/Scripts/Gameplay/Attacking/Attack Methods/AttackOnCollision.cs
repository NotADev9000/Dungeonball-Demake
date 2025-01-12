using System.Collections.Generic;
using UnityEngine;

public class AttackOnCollision : MonoBehaviour
{
    private IHaveATeam teamOwner;

    private AttackHandler _attackHandler;

    // NOTE: actions are not dependent on the attack being active so will always be executed on collision
    public List<IAmActionable> _onCollisionNormalActions;
    public List<IAmCollisionAction> _onCollisionActions;

    private void Awake()
    {
        teamOwner = GetComponent<IHaveATeam>();
    }

    public void Init(AttackHandler attackHandler, List<IAmActionable> onCollisionNormalActions = null, List<IAmCollisionAction> onCollisionActions = null)
    {
        _attackHandler = attackHandler;
        _onCollisionNormalActions = onCollisionNormalActions ?? new List<IAmActionable>();
        _onCollisionActions = onCollisionActions ?? new List<IAmCollisionAction>();
    }

    private void OnCollisionEnter(Collision other)
    {
        ExecuteActions(_onCollisionNormalActions);
        ExecuteActions(_onCollisionActions, other);
        _attackHandler.OnAttack(other.gameObject, teamOwner.Team);
    }

    private void ExecuteActions(List<IAmActionable> actions)
    {
        foreach (IAmActionable action in actions)
        {
            action.Execute();
        }
    }

    private void ExecuteActions(List<IAmCollisionAction> actions, Collision other)
    {
        foreach (IAmCollisionAction action in actions)
        {
            action.Execute(other);
        }
    }
}
