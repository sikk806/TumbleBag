using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 데이터")]
    [SerializeField] private EnemyData enemyData;
    private HealthSystem healthSystem;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDead += HealthDeath;
    }

    #region Init
    // 컴포넌트 초기화
    private void InitComponents()
    {
        healthSystem = GetComponent<HealthSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // 필수 컴포넌트 체크
    private void CheckValidComponent()
    {
        if(healthSystem == null)
        {
            Debug.LogError("HealthSystem이 없습니다. (Enemy.cs)");
        }
        
        if(spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 없습니다. (Enemy.cs)"); 
        }
    }

    private void InitEnemyData()
    {
        if(enemyData == null || healthSystem == null) return;

        LogEnemyInfo();
    }

    private void LogEnemyInfo()
    {
        string name = enemyData.enemyName;
        Debug.Log($"Enemy : {name} 생성해야 합니다.");

    }
    #endregion

    private void HealthDeath()
    {
        Debug.Log($"나 쥬금. {gameObject.name}");
    }

    void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnDead -= HealthDeath;
        }
    }
}