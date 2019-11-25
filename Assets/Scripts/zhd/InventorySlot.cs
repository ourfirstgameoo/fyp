using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    public Button removeButton;
    public Image icon;
    public Text count;
    
    public void AddItem(KeyValuePair<Item, int> pair)
    {
        item = pair.Key;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        count.text = pair.Value.ToString();
    }
    
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        count.text = null;
    }

    public void removeItem()
    {
        Inventory.Instance.Remove(item,1);
    }


}
