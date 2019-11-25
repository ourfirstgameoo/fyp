using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParents;
    Inventory inventory;
    InventorySlot[] slots;

    
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        //inventory.OnItemChangedCallback += UpdateUI;

        slots = itemParents.GetComponentsInChildren<InventorySlot>();
        
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int num = 0;
        foreach(KeyValuePair<Item,int> kvp in inventory.items)
        {
            slots[num].AddItem(kvp);
            num++;
        }

        for(int i = num;i < slots.Length; i++)
        {
            slots[i].ClearSlot();  
        }  
    }
}
