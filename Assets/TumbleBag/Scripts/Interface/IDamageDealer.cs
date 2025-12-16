using UnityEngine;

// 데미지를 주는 객체가 사용하는 인터페이스
public interface IDamageDealer
{
    // 데미지를 주는 함수 (target : 공격 대상)
    void DealDamage(IDamageable target);

    // 데미지의 양 반환
    int GetDamageAmount();
}
