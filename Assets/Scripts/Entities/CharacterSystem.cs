using System;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maximumHealth = 100f;

    [Header("Combat")]
    [SerializeField] private float damage;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackPrepareTime;

    public event Action OnComonentsInitialized;
    
    public CharacterHealth Health { get; private set; }
    public CharacterCombat Combat { get; private set; }

    private void Awake()
    {
        Health = new CharacterHealth(maximumHealth);
        Combat = new CharacterCombat(damage, attackTime, attackPrepareTime);
        OnComonentsInitialized?.Invoke();
    }

    private void OnEnable()
    {
        Health.OnDeath += Combat.OnCharacterDeath;
    }

    private void OnDisable()
    {
        Health.OnDeath -= Combat.OnCharacterDeath;
    }
}
