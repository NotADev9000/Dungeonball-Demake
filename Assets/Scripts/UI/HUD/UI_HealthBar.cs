using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Player_Combat _playerCombat;

    [Header("UI Elements")]
    [SerializeField] private Image _healthMeter;

    private void Start()
    {
        _playerCombat.HealthComponent.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDestroy()
    {
        _playerCombat.HealthComponent.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthMeter.fillAmount = (float)currentHealth / maxHealth;
    }
}
