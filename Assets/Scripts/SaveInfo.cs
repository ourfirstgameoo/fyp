using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInfo
{
    //书院， 名字（如果有），钱，背包信息，图鉴信息
    public string college;
    //public string id;
    public int money = 0;
    public List<KeyValuePair<string, int>> itemInfo = new List<KeyValuePair<string, int>>();
    public List<string> bookInfo = new List<string>();
}
