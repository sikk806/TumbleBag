using System;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    // Attack 관련 델리게이트
    public event Action<GameObject, int> OnAttackSuccess;

    public void TryAttack(GameObject target, int damage)
    {
        HealthSystem targetHealth = target.GetComponent<HealthSystem>();

        if(targetHealth != null)
        {
            if(targetHealth.IsDead) return;

            Debug.Log($"[{gameObject.name}]이(가) [{target.name}]를 공격. (Damage : {damage})");
            targetHealth.TakeDamage(damage);

            OnAttackSuccess?.Invoke(target, damage);
        }
        else
        {
            Debug.Log($"{gameObject.name}에 targetHealth가 없음");
        }
    }
}