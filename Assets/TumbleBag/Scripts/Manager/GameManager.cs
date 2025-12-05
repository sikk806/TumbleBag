using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private HashSet<SlotUI> ActivateSlots = new HashSet<SlotUI>();

    // Singleton
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform itemsParent;

    public void OnClickStartBattle()
    {
        Debug.Log("중력 시작");

        List<DragSystem> placedItem = new List<DragSystem>();

    }

    public void AddSlot(SlotUI slot)
    {
        ActivateSlots.Add(slot);
    }

    public void RemoveSlot(SlotUI slot)
    {
        ActivateSlots.Remove(slot);
    }

}
