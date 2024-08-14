using System;
using System.Collections;
using UnityEngine;

public class CharacterCombat
{
    private float Damage { get; set; }
    private float AttackTime { get; set; }
    private float AttackPrepareTime { get; set; }

    private CharacterSystem _target;
    private bool _isPreparing;
    private bool _isAttacking;

    private Coroutine _prepareCoroutine;
    private Coroutine _attackCoroutine;

    // События для обновления UI прогресса
    public event Action<float> OnAttackPrepareProgress;
    public event Action<float> OnAttackTimeProgress;

    public CharacterCombat(float damage, float attackTime, float attackPrepareTime)
    {
        Damage = damage;
        AttackTime = attackTime;
        AttackPrepareTime = attackPrepareTime;
    }

    public void SetTarget(CharacterSystem newTarget)
    {
        _target = newTarget;
    }

    public void StartAttack()
    {
        if (_target != null && _target.Health.IsAlive() && !_isPreparing && !_isAttacking)
        {
            _prepareCoroutine = CoroutineManager.Instance.StartCoroutine(PrepareForAttack());
        }
    }

    private IEnumerator PrepareForAttack()
    {
        if (_isPreparing || _isAttacking)
        {
            yield break;
        }

        _isPreparing = true;
        float elapsedTime = 0f;

        while (elapsedTime < AttackPrepareTime)
        {
            elapsedTime += Time.deltaTime;
            OnAttackPrepareProgress?.Invoke(elapsedTime / AttackPrepareTime);
            yield return null;
        }

        _isPreparing = false;
        OnAttackPrepareProgress?.Invoke(0);
        _attackCoroutine = CoroutineManager.Instance.StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        if (_target != null && _target.Health.IsAlive())
        {
            _isAttacking = true;
            float elapsedTime = 0f;

            while (elapsedTime < AttackTime)
            {
                elapsedTime += Time.deltaTime;
                OnAttackTimeProgress?.Invoke(elapsedTime / AttackTime);
                Debug.Log(elapsedTime / AttackTime);
                yield return null;
            }

            _target.Health.TakeDamage(Damage);
            OnAttackTimeProgress?.Invoke(0);
            _isAttacking = false;
        }

        StartAttack();
    }

    private void StopCombatCoroutines()
    {
        if (_prepareCoroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_prepareCoroutine);
            _prepareCoroutine = null;
        }

        if (_attackCoroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    public void OnCharacterDeath()
    {
        StopCombatCoroutines();
    }
}
