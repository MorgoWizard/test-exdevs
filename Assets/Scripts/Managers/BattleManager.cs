using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public CharacterSystem playerCharacterSystem;
    public CharacterSystem enemyCharacterSystem;

    private void Start()
    {
        // Устанавливаем цель для каждого персонажа
        playerCharacterSystem.Combat.SetTarget(enemyCharacterSystem);
        enemyCharacterSystem.Combat.SetTarget(playerCharacterSystem);
    }

    public void StartBattle()
    {
        // Начинаем атаку каждого персонажа
        playerCharacterSystem.Combat.StartAttack();
        enemyCharacterSystem.Combat.StartAttack();
    }
}