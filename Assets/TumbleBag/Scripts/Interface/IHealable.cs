using UnityEngine;

// 회복이 가능한 객체가 구현하는 인터페이스
public interface IHealable
{
    // amount : 힐량
    void Heal(int amount);

    bool CanBeHealed { get; }
}
