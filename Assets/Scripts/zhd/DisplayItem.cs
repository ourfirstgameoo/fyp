using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItem : MonoBehaviour
{
    public GameObject displayingItem = null;
    
    // Start is called before the first frame update
    void Awake()
    {
        displayingItem = GameObject.Instantiate(ItemMng.Instance.currentItem.objPrefeb,this.transform);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
