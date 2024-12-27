using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Damageable : MonoBehaviour
{
    private HealthComponent _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    public void Damage(int damageAmount)
    {
        _healthComponent.ChangeCurrentHealth(damageAmount);
    }
}
