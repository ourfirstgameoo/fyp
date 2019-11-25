using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoSingleton<Shop>
{
    public Dictionary<Item, Dictionary<Item,int>> shopMenu = new Dictionary<Item, Dictionary<Item,int>>();
    // Start is called before the first frame update
    public void Start()
    {
        LoadShopData();
        
    }
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }


    private void LoadShopData()
    {
        List<Item> origin = new List<Item>();
        List<Item> hilevel = new List<Item>();

        foreach (var kvp in ItemMng.Instance.itemData)
        {
            if (kvp.Value.itemtype == Item.itemType.origin)
            {
                if (!kvp.Value.isDefaultItem)
                    origin.Add(kvp.Value);

            }
            else
            {
                hilevel.Add(kvp.Value);
                Debug.Log("before " + kvp.Key);
                Debug.Log("im loading! " + kvp.Value.name);
            }
        }
        foreach(var i in hilevel)
        {
            Dictionary<Item, int> temp = new Dictionary<Item, int>();
            for(int t = 0; t < origin.Count; t++)
            {
                var num = UnityEngine.Random.Range(0, 3);
                if (num == 0)
                    continue;
                temp.Add(origin[t], num);
            }
            shopMenu.Add(i, temp);
        }
    }
}

