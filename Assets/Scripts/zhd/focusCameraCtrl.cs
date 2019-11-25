using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusCameraCtrl : MonoBehaviour
{

    public int distance = 10;
    
    public void changePosition(Transform building, Vector3 playerPos)
    {
        var diff = playerPos - building.position;
        transform.position = building.position + diff.normalized * distance;
        transform.LookAt(building);
    }

}
