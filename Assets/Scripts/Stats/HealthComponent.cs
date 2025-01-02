using System;
using UnityEngine;

[Serializable]
public class HealthComponent
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    public event Action OnDeath;

    private void ChangeCurrentHealth(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDeath?.Invoke();
        }

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        // Debug.Log(gameObject.name + " Health: " + _currentHealth);
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