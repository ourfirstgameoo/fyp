using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtl : MonoBehaviour
{

    private bool gpsEnabled;
    private Vector3 virturlLoc = new Vector3(0,4,0);
    public int playerMode;
    public GameObject ball;
    public GameObject camera;
    public float force = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        gpsEnabled = EnableGps();
        Input.location.Start(10f, 10f);
    }

    private bool EnableGps()
    {
        if (Input.location.isEnabledByUser)
        {
            virturlLoc.x = Input.location.lastData.latitude;
            virturlLoc.z = Input.location.lastData.longitude;
            transform.position = virturlLoc - GetComponent<MapReader>().bounds.center;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        if(playerMode == 1)
        {
            Attack();
        }
    }

    private void Attack()
    {
        GameObject a = GameObject.Instantiate(ball, transform.position + new Vector3(0,1.5f,0), transform.rotation);
        a.GetComponent<Rigidbody>().AddForce(camera.transform.forward * force);
        playerMode = 0;
    }

    private void Move()
    {
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Input.location.Start(10f,10f);
        }
        Debug.Log((Input.location.lastData.longitude - 0.003f) + "  "+(Input.location.lastData.latitude + 0.0025f));
        virturlLoc.x = (float)(MercatorProjection.lonToX(Input.location.lastData.longitude - 0.003f - 0.00044f));
        virturlLoc.z = (float)(MercatorProjection.latToY(Input.location.lastData.latitude + 0.0025f + 0.00047f));
        Debug.Log("center"+virturlLoc);

        transform.position = virturlLoc - GameObject.Find("Map").GetComponent<MapReader>().bounds.center;
    }
    public void switchMode(int a)
    {
        playerMode = a;
    }
}
