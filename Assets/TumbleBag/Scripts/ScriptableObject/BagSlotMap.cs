using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "BagSlotMap", menuName = "Scriptable Objects/BagSlotMap")]
public class BagSlotMap : ScriptableObject
{
    [Header("Bag Size")]
    public int width = 5;
    public int height = 5;

    [Header("Place Slots")]
    [Tooltip("0. null, 1. Slot, size : width * height")]
    public int[] layoutMap;

    public bool HasSlotAt(int x, int y)
    {
        if(x < 0 || x >= width || y < 0 || y >= height) return false;

        int index = y * width + x;

        if(index >= 0 && index < layoutMap.Length) return layoutMap[index] == 1;
        return false;
    }

}
