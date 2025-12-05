using NUnit.Framework.Internal;
using UnityEngine;

// Slot 관련 클래스
public class SlotUI : MonoBehaviour
{
    public DragSystem currentItem = null;
    private SpriteRenderer slotRenderer;

    public int gridX { get; private set; }
    public int gridY { get; private set; }

    void Awake()
    {
        slotRenderer = GetComponent<SpriteRenderer>();
        if(currentItem == null) ShowSlot();
    }

    void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.AddSlot(this);
        }
    }

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public void SetItem(DragSystem item)
    {
        this.currentItem = item;
        item.transform.SetParent(this.transform);
        item.transform.localPosition = Vector3.zero;
    }

    public void RemoveItem()
    {
        this.currentItem = null;
    }

    public void ShowSlot()
    {
        ChangeAlpha(1f);
    }

    public void HideSlot()
    {
        ChangeAlpha(0f);
    }

    public void HoverSlot()
    {
        ChangeAlpha(0.5f);
    }

    public void SetCoordinate(int x, int y)
    {
        this.gridX = x;
        this.gridY = y;
    }

    private void ChangeAlpha(float alpha)
    {
        if(slotRenderer != null)
        {
            Color tmpColor = slotRenderer.color;
            tmpColor.a = alpha;
            slotRenderer.color = tmpColor;
        }
    }
}
