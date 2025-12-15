using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    [Tooltip("적의 이름")]
    public string enemyName = "Slime";
    
    [Tooltip("적의 아이콘/스프라이트")]
    public Sprite icon;
    
    [TextArea(3, 5)]
    [Tooltip("적 설명")]
    public string description;
    
    [Header("Stats")]
    [Tooltip("최대 체력")]
    [SerializeField] private int maxHp = 10;
    
    [Tooltip("공격력 (추후 적이 공격할 때 사용)")]
    public int attackDamage = 1;
    
    [Tooltip("처치 시 보상 골드")]
    public int rewardGold = 10;
    
    [Header("Visual Effects - Damage")]
    [Tooltip("피격 시 플래시 색상")]
    public Color damageFlashColor = Color.red;
    
    [Tooltip("피격 플래시 지속 시간")]
    [Range(0.05f, 0.5f)]
    public float flashDuration = 0.1f;
    
    [Tooltip("피격 시 카메라 흔들림 시간")]
    [Range(0.05f, 0.5f)]
    public float damageShakeDuration = 0.15f;
    
    [Tooltip("피격 시 카메라 흔들림 강도")]
    [Range(0.1f, 1f)]
    public float damageShakeMagnitude = 0.2f;
    
    [Header("Visual Effects - Death")]
    [Tooltip("사망 시 카메라 흔들림 시간")]
    [Range(0.1f, 1f)]
    public float deathShakeDuration = 0.3f;
    
    [Tooltip("사망 시 카메라 흔들림 강도")]
    [Range(0.2f, 2f)]
    public float deathShakeMagnitude = 0.5f;
    
    [Tooltip("사망 후 오브젝트 제거 딜레이")]
    [Range(0f, 2f)]
    public float destroyDelay = 0.2f;
    
    [Header("Audio (추후 구현)")]
    public AudioClip damageSound;
    public AudioClip deathSound;
    
    public int MaxHp => maxHp;
    
    void OnValidate()
    {
        ValidateMaxHp();
        ValidateAttackDamage();
        ValidateRewardGold();
    }
    
    private void ValidateMaxHp()
    {
        if (maxHp < 1)
        {
            maxHp = 1;
            Debug.LogWarning($"[EnemyData] {name}의 maxHp는 최소 1이어야 합니다.");
        }
    }
    
    private void ValidateAttackDamage()
    {
        if (attackDamage < 0)
        {
            attackDamage = 0;
            Debug.LogWarning($"[EnemyData] {name}의 attackDamage는 0 이상이어야 합니다.");
        }
    }
    
    private void ValidateRewardGold()
    {
        if (rewardGold < 0)
        {
            rewardGold = 0;
            Debug.LogWarning($"[EnemyData] {name}의 rewardGold는 0 이상이어야 합니다.");
        }
    }
}
