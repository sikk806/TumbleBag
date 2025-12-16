using UnityEngine;

// 데미지를 받는 객체가 사용하는 인터페이스
public interface IDamageable
{
    // target : 공격하는 대상
    void DealDamage(IDamageable target);

    // 살아있는지 체크
    bool IsAlive { get; }

    // 데미지를 받을 수 있는 상태인지 체크 (ex. 무적 상태 일수도?)
    bool CanTakeDamage { get; }
}
