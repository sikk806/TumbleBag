using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Scriptable Objects/WeaponItemData")]
public class WeaponItemData : ItemData
{
    [Header("Weapon Stat")]
    public int attackDamage = 1;
}
