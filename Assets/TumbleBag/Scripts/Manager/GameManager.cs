using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsBattleStarted { get; private set; } = false;
    public HashSet<SlotUI> ActivateSlots = new HashSet<SlotUI>();
    public List<DragSystem> ActiveItems { get; private set; } = new List<DragSystem>();
    public Transform ItemContainer;

    [Header("카메라 흔들림 조절")]
    public float cameraShakeDuration = 0.2f;
    public float cameraShakeMagnitude = 0.3f;

    // Singleton
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Start!!!");
            if (!IsBattleStarted)
            {
                StartBattle();
            }
            else
            {
                if(ShakeManager.Instance != null)
                {
                    ShakeManager.Instance.ShakeItems();
                }

                if(ShakeSystem.Instance != null)
                {
                    ShakeSystem.Instance.Shake(cameraShakeDuration, cameraShakeMagnitude);
                }
            }
        }
    }

    public void AddSlot(SlotUI slot)
    {
        ActivateSlots.Add(slot);
    }

    public void RemoveSlot(SlotUI slot)
    {
        ActivateSlots.Remove(slot);
    }

    public void StartBattle()
    {
        if (IsBattleStarted) return;

        IsBattleStarted = true;

        ActiveItems.Clear();

        foreach (SlotUI slot in ActivateSlots)
        {
            if (!slot.IsEmpty())
            {
                ActiveItems.Add(slot.currentItem);
                slot.currentItem = null;
            }
        }

        foreach (DragSystem item in ActiveItems)
        {
            // 아이템도 사라지면 ItemContainer 추가해야한다.
            if(ItemContainer != null)
            {
                item.transform.SetParent(ItemContainer);
            }
            item.StartBattle();
        }

        foreach(SlotUI slot in ActivateSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }

}
