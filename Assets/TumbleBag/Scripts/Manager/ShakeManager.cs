using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance { get; private set; }
    [Header("흔들기 강도 설정")]
    [Tooltip("위로 튀어오르는 힘")]
    [SerializeField] private float upwardForce = 12f;
    [Tooltip("좌우로 퍼지는 힘")]
    [SerializeField] private float sideForceRange = 6f;
    [Tooltip("돌리는 힘")]
    [SerializeField] private float torqueRange = 100f;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void ShakeItems()
    {
        if(GameManager.Instance == null)
        {
            Debug.LogError("GameManager가 없음!! (ShakeManager -> ShakeItems)");
            return;
        }
        Debug.Log("ShakeManger -> ShakeItems!");

        List<DragSystem> activeItems = GameManager.Instance.ActiveItems;

        foreach(DragSystem item in activeItems)
        {
            if(item == null) continue;

            Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();

            if(rigid != null && rigid.bodyType == RigidbodyType2D.Dynamic)
            {
                // 기존 속도를 0으로 만들기 (힘을 일정하게 받는 것을 구현)
                rigid.linearVelocity = Vector2.zero;
                rigid.angularVelocity = 0f;

                // 힘의 방향 계선하기
                float randomSide = Random.Range(-sideForceRange, sideForceRange);
                Vector2 shakeForce = new Vector2(randomSide, upwardForce);

                // 랜덤 회전력 계산
                float randomTorque = Random.Range(-torqueRange, torqueRange);

                // 순간적인 힘 가하기
                rigid.AddForce(shakeForce, ForceMode2D.Impulse);
                rigid.AddTorque(randomTorque, ForceMode2D.Impulse);
            }
        }   
    }
}
