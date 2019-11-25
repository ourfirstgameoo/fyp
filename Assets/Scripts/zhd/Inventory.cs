using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{

    public Dictionary<Item,int> items = new Dictionary<Item, int>();
    public int space = 24;
    /*
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    */

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            return false;
        }
        if (items.ContainsKey(item))
        {
            items[item]++;
        }
        else
        {
            items.Add(item, 1);
        }
/*
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();

        }*/
        Debug.Log("add");

        return true;
    }

    public void Remove(Item item,int i)
    {
        items[item]-= i;
        if(items[item] <= 0)
        {
            items.Remove(item);
        }
        /*
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
            */
    }


}
