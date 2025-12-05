using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private WeaponItemData weaponData;

    private AttackSystem attackSystem;

    public int CurrentDamage
    {
        get
        {
            if(weaponData == null)
            {
                Debug.LogError($"{gameObject.name}에 WeaponData가 없음.");
                return 0;
            }

            return weaponData.attackDamage;
        }
    }
    void Start()
    {
        attackSystem = GetComponent<AttackSystem>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            attackSystem.TryAttack(collision.gameObject, this.CurrentDamage);
        }
    }
}
