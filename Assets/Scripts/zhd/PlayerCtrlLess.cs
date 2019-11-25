using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrlLess : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 virturlLoc = new Vector3(0, 4, 0);
    private Vector3 center;
    private bool gpsEnabled;
    public Text text;

    void Start()
    {
        center = GameObject.Find("Map").GetComponent<MapReader>().bounds.center;
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            //Debug.Log("user has no enabled gps");
            yield break;
        }
        Input.location.Start();

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            //Debug.Log("unable to determine device location");
            yield break;
        }

        virturlLoc.x = (float)(MercatorProjection.lonToX(Input.location.lastData.longitude));
        virturlLoc.z = (float)(MercatorProjection.latToY(Input.location.lastData.latitude));
        transform.position = virturlLoc - center;

        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        //updateLocation();

        noLocationService();


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            LayerMask mask = 1 << 10;//building layer
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,mask))
            {
                //Debug.Log("hit!!"+hit.transform.GetComponent<Building>().attr.name);
                //Debug.Log(hit.transform.tag);
            }
        }
    }

    private void noLocationService()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void updateLocation()
    {
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Input.location.Start(10f, 10f);
        }

        virturlLoc.x = (float)(MercatorProjection.lonToX(Input.location.lastData.longitude));
        virturlLoc.z = (float)(MercatorProjection.latToY(Input.location.lastData.latitude));
        transform.position = virturlLoc - GameObject.Find("Map").GetComponent<MapReader>().bounds.center;
    }

    private void OnTriggerEnter(Collider other)
    {
        Building build = other.GetComponent<Building>();
        if(build != null)
        {
            text.text = build.attr.name;
            switch (build.attr.region) {
                case BuildingAttr.RegionType.Campus:
                    ItemMng.Instance.changeCurrent("red");
                    break;
                case BuildingAttr.RegionType.CC:
                case BuildingAttr.RegionType.CWC:
                case BuildingAttr.RegionType.SHAW:
                case BuildingAttr.RegionType.WYS:
                    ItemMng.Instance.changeCurrent("green");
                    break;
                default:
                    ItemMng.Instance.changeCurrent("blue");
                    break;

            }

            if (UnityEngine.Random.Range(0, 1) >= 0)
            {
                GameObject.Find("Inform").GetComponent<Image>().sprite = ItemMng.Instance.currentItem.icon;
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    Building build = other.GetComponent<Building>();
    //    if (build != null)
    //    {
    //        text.text = build.attr.name;
    //        switch (build.attr.region)
    //        {
    //            case BuildingAttr.RegionType.Campus:
    //                ItemMng.Instance.changeCurrent("red");
    //                break;
    //            case BuildingAttr.RegionType.CC:
    //            case BuildingAttr.RegionType.CWC:
    //            case BuildingAttr.RegionType.SHAW:
    //            case BuildingAttr.RegionType.WYS:
    //                ItemMng.Instance.changeCurrent("green");
    //                break;
    //            default:
    //                ItemMng.Instance.changeCurrent("blue");
    //                break;

    //        }

    //        if (UnityEngine.Random.Range(0, 1) >= 0.5)
    //        {
    //            GameObject.Find("Inform").GetComponent<Image>().sprite = ItemMng.Instance.currentItem.icon;
    //        }
    //    }
    //}
}
