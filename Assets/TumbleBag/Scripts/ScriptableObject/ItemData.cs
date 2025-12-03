using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
}
