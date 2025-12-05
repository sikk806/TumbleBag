using UnityEngine;
using UnityEngine.EventSystems;

// 아이템을 드래그할 때 사용되는 클래스
// 아이템을 1. 들어서 2. 드래그해서 3. Slot에 넣기.
public class DragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public WeaponItemData itemData; // 모양을 담고있는 데이터가 필요
    private SlotUI originalSlot;
    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private SlotUI lastHoveredSlot = null;

    void Start()
    {
        mainCamera = Camera.main;
        canvasGroup = GetComponent<CanvasGroup>();

        if(canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        originalSlot = GetComponentInParent<SlotUI>();
        if(originalSlot != null && GridManager.Instance != null && itemData != null)
        {
            GridManager.Instance.PlaceItem(originalSlot.gridX, originalSlot.gridY, this, itemData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 시작");
        if (originalSlot != null)
        {
            originalSlot.RemoveItem();
        }
        
        Transform slotContainer = transform.parent.parent; 
        transform.SetParent(slotContainer.parent); 

        canvasGroup.blocksRaycasts = false;
        ChangeSpriteAlpha(0.6f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        mouseWorldPos.z = 0f;
        transform.position = mouseWorldPos;

        SlotUI nearestSlot = FindNearestSlot();

        if(nearestSlot != lastHoveredSlot)
        {
            // 이전 호버된 슬롯 -> 불투명하게
            if(lastHoveredSlot != null) lastHoveredSlot.ShowSlot();
            // 비어있는 가까운 슬롯 -> 호버링
            if(nearestSlot != null && nearestSlot.IsEmpty()) nearestSlot.HoverSlot();
            lastHoveredSlot = nearestSlot;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 종료. 스왑 시도");
        canvasGroup.blocksRaycasts = true;
        ChangeSpriteAlpha(1.0f);

        if(lastHoveredSlot != null)
        {
            lastHoveredSlot.ShowSlot();
            lastHoveredSlot = null;
        }

        // 1. 가장 가까운 슬롯 찾기
        SlotUI nearestSlot = FindNearestSlot();

        // 2. 찾았고, 그 슬롯이 비어있다면? -> 그쪽으로 이사!
        if (nearestSlot != null && nearestSlot.IsEmpty() && GridManager.Instance != null)
        {
            Debug.Log($"스냅 성공! 새 슬롯: {nearestSlot.name}");
            nearestSlot.SetItem(this); 
            originalSlot = nearestSlot;
            nearestSlot.HideSlot();
        }
        // 3. 못 찾았거나 이미 차있다면? -> 원래 슬롯으로 복귀!
        else
        {
            Debug.Log("스냅 실패. 원래 위치로 복귀.");
            if (originalSlot != null)
            {
                originalSlot.SetItem(this); // 원래 슬롯으로 돌아감
            }
        }
    }

    public void SetOriginalSlot(SlotUI slot)
    {
        this.originalSlot = slot;
    }

    private SlotUI FindNearestSlot()
    {
        SlotUI[] allSlots = FindObjectsOfType<SlotUI>();
        
        SlotUI nearest = null;
        float minDistance = float.MaxValue; 

        foreach (SlotUI slot in allSlots)
        {
            float distance = Vector3.Distance(transform.position, slot.transform.position);

            if (distance < minDistance)
            {
                if (distance < 1.5f) 
                {
                    minDistance = distance;
                    nearest = slot;
                }
            }
        }
        return nearest;
    }

    private void ChangeSpriteAlpha(float alpha)
    {
        if(spriteRenderer != null)
        {
            Color tmpColor = spriteRenderer.color;
            tmpColor.a = alpha;
            spriteRenderer.color = tmpColor;
        }
    }
}
