using UnityEngine;

public struct DamageInfo
{
    public int amount; // 데미지 양
    public DamageType damageType; // 데미지 타입
    public GameObject source; // 데미지를 준 오브젝트
    
    // 초기화
    public DamageInfo(int amount, DamageType damageType, GameObject source)
    {
        this.amount = amount;
        this.damageType = damageType;
        this.source = source;
    }

    // 기본 데미지 정보
    public static DamageInfo Create(int amount, GameObject source)
    {
        return new DamageInfo(
            amount,
            DamageType.Physical,
            source
        );
    }
}
