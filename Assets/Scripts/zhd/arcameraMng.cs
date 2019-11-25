using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arcameraMng : MonoBehaviour
{
    Camera camera = null;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        //if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
                Debug.Log("get mousedown");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 500))
            {
                Debug.Log("get raycast");
                if (hit.transform.gameObject.GetComponent<ItemAttr>() != null)
                {
                    Debug.Log("destory");
                    
                    hit.transform.gameObject.GetComponent<ItemAttr>().trackHealth -= 1;
                    if (hit.transform.gameObject.GetComponent<ItemAttr>().trackHealth == 0)
                    {
                        Destroy(hit.transform.gameObject);
                        //GameMng.Instance.ShowMap(true);
                        Inventory.Instance.Add(ItemMng.Instance.currentItem);
                        //GameObject.Find("Map").gameObject.SetActive(true);
                        SceneManager.LoadScene("MainGame");
                    }
                }
            }
        }
    }
}
