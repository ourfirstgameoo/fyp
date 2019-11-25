using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    KeyValuePair<Item, Dictionary<Item, int>> kvp;
    public Button buyButton;
    public Image target;
    public Image[] icons;
    public Text[] count;

    

   public void setSlot(KeyValuePair<Item,Dictionary<Item,int>> k)
    {
        Debug.Log("i have set slot " + k.Key.name);
        kvp = k;
        target.sprite = k.Key.icon;

        int i = 0;
        foreach(var temp in k.Value)
        {
            icons[i].gameObject.SetActive(true);
            icons[i].sprite = temp.Key.icon;
            count[i].text = temp.Value.ToString();
            i++;
        }

    }

    public void checkButton()
    {
        foreach(var k in kvp.Value)
        {
            if ((!Inventory.Instance.items.ContainsKey(k.Key))
                || (Inventory.Instance.items[k.Key] < k.Value))
            {
                buyButton.interactable = false;
                return;
            }
        }
        buyButton.interactable = true;
    }

    public void onBuyButton()
    {
        foreach(var k in kvp.Value)
        {
            Inventory.Instance.Remove(k.Key, k.Value); 
        }
    }
    private void Update()
    {
        if(kvp.Key != null)
            checkButton();
    }
}
