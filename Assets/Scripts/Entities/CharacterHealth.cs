using System;
using UnityEngine;

public class CharacterHealth
{
    public float MaximumHealth { get; private set; }
    public float CurrentHealth {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke();
        }
    }

    private float _currentHealth;

    public event Action OnDeath;
    
    public event Action OnHealthChanged;

    public CharacterHealth(float maximumHealth)
    {
        MaximumHealth = maximumHealth;
        CurrentHealth = maximumHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaximumHealth);
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}