using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    // Events
    public event Action OnDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void ChangeCurrentHealth(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
            _currentHealth = 0;
        }

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        // Debug.Log(gameObject.name + " Health: " + _currentHealth);
    }

    public void Heal(int healAmount)
    {
        ChangeCurrentHealth(-healAmount);
    }
}