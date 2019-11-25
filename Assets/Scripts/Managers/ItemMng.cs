using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMng : MonoSingleton<ItemMng>
{
    public Dictionary<string, Item> itemData = new Dictionary<string, Item>();
    public Item currentItem = null;
    // Start is called before the first frame update
    public void Start()
    {
        LoadItemData();
        currentItem = itemData["blue"];
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void changeCurrent(string name)
    {
        if (itemData.ContainsKey(name))
        {
            currentItem = itemData[name];

        }
        else
        {
            currentItem = itemData["blue"];
        }
    }


    private void LoadItemData()
    {
        Item[] items = Resources.LoadAll<Item>("Item");

        foreach (var i in items)
        {
            if(!itemData.ContainsKey(i.name))
               itemData.Add(i.name, i);
            Debug.Log("Loading " + i.name);
        }
    }

    
}
