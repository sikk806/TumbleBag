using UnityEngine;
using System.Collections.Generic;

public class ShakeSystem : MonoBehaviour
{
    [Header("흔들기 파워 설정")]
    [Tooltip("위로 튀어오르는 힘")]
    [SerializeField] private float upwardForce = 10f;
    [Tooltip("좌우로 퍼지는 힘")]
    [SerializeField] private float sideForceRange = 5f;
    [Tooltip("회전력 힘")]
    [SerializeField] private float torqueRange = 50f;

    private List<Rigidbody2D> itemsInBag = new List<Rigidbody2D>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShakeBag();
        }
    }

    void ShakeBag()
    {
        
    }

}