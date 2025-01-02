using UnityEngine;

public class HoppingAttackerEnemy : MonoBehaviour
{
    private MultiReactor _collisionReactor;

    private Damageable _damageable;
    [SerializeField] private HealthComponent _healthComponent;

    private AttackHandler _attackHandler;
    [SerializeField] private AttackAngleValidator _attackAngleValidator;

    private AttackOnJump _attackOnJump;

    private void Awake()
    {
        _attackHandler = new AttackHandler(_attackAngleValidator);
        _attackOnJump = new AttackOnJump(_attackHandler, gameObject);

        _damageable = new Damageable(_healthComponent);

        _collisionReactor = GetComponent<MultiReactor>();
        _collisionReactor.Init(_damageable, _attackHandler);
    }

    private void OnDisable()
    {
        _attackOnJump.Dispose();
    }
}
