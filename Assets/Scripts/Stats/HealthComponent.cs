using System;
using UnityEngine;

[Serializable]
public class HealthComponent
{
    private HealthData_SO _healthData;
    private int MaxHealth => _healthData.MaxHealth;

    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    public HealthComponent(HealthData_SO healthData)
    {
        _healthData = healthData;
        Init();
    }

    private void Init()
    {
        _currentHealth = MaxHealth;
    }

    public bool IsDead => _currentHealth <= 0;

    private void ChangeCurrentHealth(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth > MaxHealth)
            _currentHealth = MaxHealth;
    }

    public void Damage(int damageAmount)
    {
        ChangeCurrentHealth(-damageAmount);
    }

    public void Heal(int healAmount)
    {
        ChangeCurrentHealth(healAmount);
    }
}