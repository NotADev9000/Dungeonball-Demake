using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class DamageableReactor : CollideReactor
{
    private Damageable _damageableComponent;

    private void Awake()
    {
        _damageableComponent = GetComponent<Damageable>();
    }

    public override void HitSomething()
    {
        // Debug.Log(gameObject.name + " hit something!");
    }

    public override void GetHit(Team hitterTeam)
    {
        if (IsOnDifferentTeam(hitterTeam))
        {
            // Debug.Log(gameObject.name + " got hit!");
            _damageableComponent.Damage(10);
        }
    }
}