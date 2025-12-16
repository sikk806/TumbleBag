using UnityEngine;

// 지속 데미지를 받을 수 있는 객체가 구현하는 인터페이스
public interface IDamageOverTime
{
    // damagePerTick : 틱당 데미지, duration : 지속 시간, tickInterval : 틱 간격
    void ApplyDamageOverTime(int damagePerTick, float duration, float tickInterval);

    // 지속 데미지 효과 제거
    void RemvoeDamageOverTime();
}
