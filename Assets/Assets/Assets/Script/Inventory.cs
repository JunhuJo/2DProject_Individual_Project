using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public List<SlotData> slots = new List<SlotData>();
   public int maxSlot = 3;
   public GameObject slotPrefab;
  
    private void Start()
    {
        GameObject slotPanel = GameObject.Find("Slot");

        for (int i = 0; i < maxSlot; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);
            go.name = "Slot_" + i;
            SlotData slot = go.AddComponent<SlotData>();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }
    }
}

