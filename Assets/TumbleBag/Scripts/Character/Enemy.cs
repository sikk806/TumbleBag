using System;
using System.Collections;
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
        InitComponents();
        CheckValidComponent();
        InitEnemyData();
        SubscribeEvnets();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
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
        string name = enemyData != null ? enemyData.enemyName : "Unknown";
        Debug.Log($"Enemy : {name} 생성해야 합니다.");

    }
    #endregion

    #region Events
    
    private void SubscribeEvnets()
    {
        if(healthSystem == null) return;

        healthSystem.OnTakeDamage += HandleTakeDamage;
        healthSystem.OnDead += HandleDeath;
    }

    private void UnsubscribeEvents()
    {
        if(healthSystem == null) return;

        healthSystem.OnTakeDamage -= HandleTakeDamage;
        healthSystem.OnDead -= HandleDeath;
    }

    #endregion

    #region Collision

    void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    private void ProcessCollision(Collision2D collision)
    {
        // I DamageDealer : 데미지를 줄 수 있는 객체가 구현하는 인터페이스
        // 데미지를 받기 위해서 Enemy한테 데미지를 주는 객체가 IDamagerDealer를 가지고 있는지 체크
        IDamageDealer damageDealer = collision.gameObject.GetComponent<IDamageDealer>();

        if(damageDealer != null)
        {
            
            HandleDamageDealerCollision(damageDealer, collision);
        }
    }

    private void HandleDamageDealerCollision(IDamageDealer damageDealer, Collision2D collision)
    {
        IDamageable damageable = healthSystem as IDamageable;
        // null이 되는 경우 healthSystem을 가진 오브젝트가 IDamageable은 없다는 의미.
        if(damageable != null)
        {
            damageDealer.DealDamage(damageable);
        }
        else
        {
            Debug.LogError("IDamageable이 붙어있나 확인해보기");
        }
    }

    #endregion

    #region Damage

    private void HandleTakeDamage(int damage)
    {
        PlayDamageEffects();
    }

    private void PlayDamageEffects()
    {
        PlayDamageFlash();
    }

    private void PlayDamageFlash()
    {
        if(spriteRenderer == null) return;

        Color flashColor = enemyData != null ? enemyData.damageFlashColor : Color.red;
        float duration = enemyData != null ? enemyData.flashDuration : 0.1f;

        StartCoroutine(FlashEffect(flashColor, duration));
    }

    private IEnumerator FlashEffect(Color flashColor, float duration)
    {
        if(spriteRenderer == null) yield break;

        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    #endregion

    #region Death

    private void HandleDeath()
    {
        /*
        TODO

        죽음 이펙트
        게임 매니저에 알리기
        적 파괴
        */
    }

    #endregion

}