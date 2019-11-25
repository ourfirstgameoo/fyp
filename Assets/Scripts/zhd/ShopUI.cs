using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform listParents;
    ShopSlot[] slots;
    void Start()
    {
        slots = listParents.GetComponentsInChildren<ShopSlot>();
        
    }
    private void Update()
    {
        setSlots();
    }
    // Update is called once per frame
    private void setSlots()
    {
        int i = 0;
        foreach (var kvp in Shop.Instance.shopMenu)
        {
            slots[i].setSlot(kvp);
            i++;
        }
    }
}
