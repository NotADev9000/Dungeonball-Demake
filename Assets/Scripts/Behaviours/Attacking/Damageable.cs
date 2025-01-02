using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable
{
    private HealthComponent _healthComponent;

    public Damageable(HealthComponent healthComponent)
    {
        _healthComponent = healthComponent;
    }

    public void Damage(int damageAmount)
    {
        _healthComponent.Damage(damageAmount);
    }
}
