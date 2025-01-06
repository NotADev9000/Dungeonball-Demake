using UnityEngine;

public class Throwable : MonoBehaviour, IHaveATeam
{
    [field: SerializeField] public Teams Team { get; set; }

    private AttackHandler _attackHandler;
    [SerializeField] private AttackSpeedValidator _attackSpeedValidator;

    private void Awake()
    {
        _attackHandler = new(_attackSpeedValidator)
        {
            IsAttackActive = true
        };
    }

    private void OnCollisionEnter(Collision other)
    {
        _attackHandler.OnAttack(other.gameObject, Team);
    }
}
