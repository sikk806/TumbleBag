using UnityEngine;

// 아이템 관련 데이터
// 아이템이 얼마나 그리드를 차지하는지, 아이템의 기본 정보들이 들어있음.
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;

    [Header("아이템 모양")]
    public int widthCells;
    public int heightCells;
    [Tooltip("모양 배열 (0 : 비어있음, 1 : 점유). 크기는 width * height")]
    public bool[] shapeMap;

    void OnValidate()
    {
        if(widthCells < 1) widthCells = 1;
        if(heightCells < 1) heightCells = 1;
        int shapeSize = widthCells * heightCells;

        if(shapeMap == null | shapeMap.Length != shapeSize)
        {
            bool[] newMap = new bool[shapeSize];
            
            if(shapeMap != null && shapeMap.Length > 0)
            {
                int copyLimit = Mathf.Min(shapeSize, shapeMap.Length);

                for(int i = 0; i < copyLimit; i++)
                {
                    newMap[i] = shapeMap[i];
                }
            }

            int startIndex = (shapeMap == null) ? 0 : Mathf.Min(shapeSize, shapeMap.Length);
            for(int i = startIndex; i < shapeSize; i++)
            {
                newMap[i] = true;
            }

            shapeMap = newMap;
        }
    }
}
