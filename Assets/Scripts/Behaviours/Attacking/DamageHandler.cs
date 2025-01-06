using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler
{
    private HealthComponent _healthComponent;

    public DamageHandler(HealthComponent healthComponent)
    {
        _healthComponent = healthComponent;
    }

    // TODO: if time, add checks for type of damage (e.g. weapon type)
    public bool TryDamage(int damageAmount)
    {
        _healthComponent.Damage(damageAmount);
        return true;
    }
}
