using System.Xml.Schema;
using UnityEngine;

// 그리드를 만들 때 사용되는 클래스
// Slot의 크기를 조정해줌
public class GridSystem : MonoBehaviour
{
    [Header("Setting")]
    public BagSlotMap currentBagShape;

    [Header("Prefabs")]
    public Transform slotContainer;
    public GameObject slotPrefab;
    public GameObject monsterPrefab;
    public SpriteRenderer slotBox; // slot이 채워질 공간 (가장 큰 네모 공간)

    private GridManager gridManager;

    void Start()
    {
        gridManager = GetComponent<GridManager>();
        if(gridManager == null)
        {
            Debug.LogError("GridManager이 없음.");
            return;
        }

        GridInit();
    }

    void GridInit()
    {
        if (currentBagShape == null || slotContainer == null || slotContainer == null || slotBox == null || monsterPrefab == null)
        {
            Debug.Log("No Objects in GrideManager.cs! (currentBagShape ||  slotContainer || slotPrefab || slotBox || monsterPrefab)");
            return;
        }

        // get Slot's Sprite Renderer
        SpriteRenderer prefabRenderer = slotPrefab.GetComponent<SpriteRenderer>();
        if (prefabRenderer == null) return;

        SpriteRenderer monsterprefabRenderer = monsterPrefab.GetComponent<SpriteRenderer>();
        if (monsterprefabRenderer == null) return;

        // SO에서 그리드 개수 가져옴. (Ex. 가로, 세로 줄 수 가져오는 것)
        int gridCol = currentBagShape.width;
        int gridRow = currentBagShape.height;

        // 가방의 스프라이트 가로, 세로 길이 가져옴.
        float availableWidth = slotBox.bounds.size.x;
        float availableHeight = slotBox.bounds.size.y;

        // 그리드에 넣을 슬롯 길이 계산
        float targetCellWidth = availableWidth / gridCol;
        float targetCellHeight = availableHeight / gridRow;

        // 그리드 정보 초기화
        // 가로, 세로 크기와 타겟 셀의 크기(좌표 계산에 사용) 넘겨줌
        gridManager.InitGrid(gridCol, gridRow, targetCellWidth);

        // 원본 슬롯의 크기 가져옴
        float originalSlotWidth = prefabRenderer.bounds.size.x;
        float originalSlotHeight = prefabRenderer.bounds.size.y;

        // 스케일 팩터 계산 (목표 크기 / 원본 크기 = 몇 배 적용해야하는지)
        float scaleX = targetCellWidth / originalSlotWidth;
        float scaleY = targetCellHeight / originalSlotHeight;

        float originalMonsterWidth = prefabRenderer.bounds.size.x;
        float originalMonsterHeight = prefabRenderer.bounds.size.y;

        float monsterScaleX = targetCellWidth / originalMonsterWidth;
        float monsterScaleY = targetCellHeight / originalMonsterHeight;

        // 최종 적용할 스케일
        Vector3 targetScale = new Vector3(scaleX, scaleY, 1f);

        Vector3 monsterScale = new Vector3(monsterScaleX * 1.1f, monsterScaleY * 1.1f, 1f);

        // 중앙 정렬을 위한 오프셋
        float xOffset = (gridCol - 1) / 2.0f;
        float yOffset = (gridRow - 1) / 2.0f;

        for (int x = 0; x < gridCol; x++)
        {
            for (int y = 0; y < gridRow; y++)
            {
                if (!currentBagShape.HasSlotAt(x, y)) continue;

                float posX = (x - xOffset) * targetCellWidth;
                float posY = ((gridRow - 1 - y) - yOffset) * targetCellHeight;

                Vector3 localPos = new Vector3(posX, posY, 0);
                Vector3 spawnPos = slotContainer.position + localPos;

                // monster 배치. 나중에는 랜덤으로 들어갈 수 있도록 할것.
                if (x == 3 && y == 3)
                {
                    GameObject monsterSlot = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
                    monsterSlot.transform.localScale = monsterScale;
                    monsterSlot.transform.SetParent(slotContainer);

                    monsterSlot.name = $"MonterSlot";
                    continue;
                }
                GameObject spawnedSlot = Instantiate(slotPrefab, spawnPos, Quaternion.identity);

                spawnedSlot.transform.localScale = targetScale;
                spawnedSlot.transform.SetParent(slotContainer);

                spawnedSlot.name = $"Slot({y}_{x})";

                // SlotUI를 GridManager에 등록
                SlotUI slotUI = spawnedSlot.GetComponent<SlotUI>();
                if(slotUI != null)
                {
                    gridManager.RegisterSlot(x, y, slotUI);
                }
            }
        }
    }
}
