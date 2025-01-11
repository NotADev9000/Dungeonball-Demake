using UnityEngine;

public class AttackOnCollision : MonoBehaviour
{
    private IHaveATeam teamOwner;

    private AttackHandler _attackHandler = new();

    private void Awake()
    {
        teamOwner = GetComponent<IHaveATeam>();
    }

    public void Init(AttackHandler attackHandler)
    {
        _attackHandler = attackHandler;
    }

    private void OnCollisionEnter(Collision other)
    {
        _attackHandler.OnAttack(other.gameObject, teamOwner.Team);
    }
}
