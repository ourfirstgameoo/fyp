using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttr : MonoBehaviour
{
    public int trackHealth = 3;

    public bool destroyOnce()
    {
        trackHealth--;
        if (trackHealth == 0)
        {
            Inventory.Instance.Add(ItemMng.Instance.currentItem);
            return false;
        }
        return true;
    }
}
