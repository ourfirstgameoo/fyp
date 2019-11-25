using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {    
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("!!!!");
        if(other.tag == "plane")
        {
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position,new Quaternion(0,0,0,0));
            Destroy(effect, 3f);
            Destroy(this.gameObject);
        }
    }
}
