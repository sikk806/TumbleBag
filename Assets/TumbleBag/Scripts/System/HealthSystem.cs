using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private int maxHp = 10;
    private int currentHp;

    // 데미지 입었을 때 데미지 매개변수를 넘겨받는다.
    public event Action<int> OnTakeDamage;

    // 죽었을 때 발동
    public event Action OnDead;

    public int CurrentHp => currentHp;
    public int MaxHp => maxHp; 
    public bool IsDead => (CurrentHp < 1);

    void Start()
    {
        InitHealth();
    }

    public void InitHealth()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if(IsDead) return;

        currentHp -= damage;
        Debug.Log($"아야!!! (damage : {damage}, currentHp : {currentHp})");

        OnTakeDamage?.Invoke(damage);

        if(currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("X_X 꽥!");
        OnDead?.Invoke();
    }
}