using System.Threading;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private SlotUI[,] slotGrid;
    private int gridWidth;
    private int gridHeight;

    public float CellSize { get; private set; }

    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // GirdSystem에서 슬롯을 다 만든 후에 호출해줄 것.
    public void InitGrid(int width, int height, float cellSize)
    {
        gridWidth = width;
        gridHeight = height;
        this.CellSize = cellSize;
        slotGrid = new SlotUI[width, height];
    }

    public void RegisterSlot(int x, int y, SlotUI slot)
    {
        if (IsValidCoordinate(x, y))
        {
            slotGrid[x, y] = slot;
            slot.SetCoordinate(x, y);
        }
    }

    public bool CanPlaceItem(int targetX, int targetY, ItemData itemData)
    {
        if (slotGrid == null) return false;
        if (itemData == null)
        {
            Debug.LogError("No itemData");
        }
        for (int dy = 0; dy < itemData.heightCells; dy++)
        {
            for (int dx = 0; dx < itemData.widthCells; dx++)
            {
                int shapeIndex = dy * itemData.widthCells + dx;

                if (itemData.shapeMap[shapeIndex])
                {
                    int gridX = targetX + dx;
                    int gridY = targetY + dy;

                    if (!IsValidCoordinate(gridX, gridY)) return false;

                    SlotUI slot = slotGrid[gridX, gridY];
                    if (slot == null || !slot.IsEmpty()) return false;
                }
            }
        }

        return true;
    }

    public void PlaceItem(int targetX, int targetY, DragSystem item, ItemData itemData)
    {
        if (slotGrid == null) return;
        // 먼저 모든 occupied 슬롯을 찾고 월드 위치의 평균을 구함
        Vector3 totalWorldPos = Vector3.zero;
        int occupiedCount = 0;

        for (int dy = 0; dy < itemData.heightCells; dy++)
        {
            for (int dx = 0; dx < itemData.widthCells; dx++)
            {
                int shapeIndex = dy * itemData.widthCells + dx;

                if (itemData.shapeMap[shapeIndex])
                {
                    int gridX = targetX + dx;
                    int gridY = targetY + dy;

                    SlotUI slot = slotGrid[gridX, gridY];
                    slot.currentItem = item;
                    slot.HideSlot();

                    // 각 슬롯의 월드 위치를 누적
                    totalWorldPos += slot.transform.position;
                    occupiedCount++;
                }
            }
        }

        if (occupiedCount > 0)
        {
            // occupied 슬롯들의 중심 위치 (월드 좌표)
            Vector3 centerWorldPos = totalWorldPos / occupiedCount;
            centerWorldPos.z = 0;

            // 아이템을 슬롯 컨테이너의 자식으로 설정
            SlotUI anchorSlot = slotGrid[targetX, targetY];
            item.transform.SetParent(anchorSlot.transform.parent);

            // 월드 위치로 배치
            item.transform.position = centerWorldPos;

            item.SetOriginalSlot(anchorSlot);

            Debug.Log($"Item World Position: {centerWorldPos}");
            Debug.Log($"Occupied slots: {occupiedCount}");
        }
    }

    public void UnPlaceItem(int targetX, int targetY, ItemData itemData)
    {
        if (slotGrid == null) return;

        for (int dy = 0; dy < itemData.heightCells; dy++)
        {
            for (int dx = 0; dx < itemData.widthCells; dx++)
            {
                int shapeIndex = dy * itemData.widthCells + dx;

                if (itemData.shapeMap[shapeIndex])
                {
                    int currentX = targetX + dx;
                    int currentY = targetY + dy;

                    if (IsValidCoordinate(currentX, currentY))
                    {
                        SlotUI slot = slotGrid[currentX, currentY];

                        if (slot != null)
                        {
                            slot.RemoveItem();
                            slot.ShowSlot();
                        }
                    }
                }
            }
        }
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }
}
