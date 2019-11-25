using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCtl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraParent;
    public Quaternion rot = new Quaternion(0, 0, 1, 0);
    public Vector3 _cameraOffset;
    public Transform PlayerTransform;

    [Range(0.01f,1.0f)]
    public float smoothFactor = 0.5f;

    public int currentMode = 0;

    //scale parameter
    private float distance = 10.0f;
    public float tempdistance = 0.0f;

    // origin two finger position
    private Vector2 oldPosition1 = new Vector2(0, 0);
    private Vector2 oldPosition2 = new Vector2(0, 0);


    // Start is called before the first frame update
    void Start()
    {
        cameraParent = new GameObject("Camera parent");
        cameraParent.transform.position = this.transform.position;
        transform.SetParent(cameraParent.transform);
        gyroEnabled = EnableGyro();

        _cameraOffset = new Vector3(0, 3.0f,-10.0f);
        
    }



    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraParent.transform.rotation = Quaternion.Euler(90, 90, 0);
            return true;
        }
        else
        {
            return false;

        }
    }

    void Update()
    {
        switch (currentMode)
        {
            case 0:
                // regular move
                {
                    FollowPlayer();
                    break;
                }
            case 1:
                {
                    Throw();
                    break;
                }
            case 2:
                {
                    overLook();
                    break;
                }

        }

    }


    void overLook()
    {
        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;

                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    if (distance > 0)
                        distance -= 4.0f;
                }
                else
                {
                    if (distance < 100f)
                    {
                        distance += 4.0f;
                    }
                }
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }
        }
        Vector3 newPos = PlayerTransform.position + new Vector3(0, 70 + distance, 0);
        transform.position = Vector3.Slerp(transform.position, newPos , smoothFactor);
        transform.rotation = PlayerTransform.rotation;
        transform.Rotate(90, 0, 0);
        
        
    }

    private bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {

        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        if (leng1 < leng2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    void FollowPlayer()
    {
        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                oldPosition1 = Input.GetTouch(0).position;

            if (Input.GetTouch(1).phase == TouchPhase.Began)
                oldPosition1 = Input.GetTouch(1).position;

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;

                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    if (distance > 0)
                        distance -= 2.0f;
                }
                else
                {
                    if (distance < 100f)
                    {
                        distance += 2.0f;
                    }
                }
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }

            Vector3 newPos = PlayerTransform.position + _cameraOffset + new Vector3(0, distance, -distance);
            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
            
        }

        else if(Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                oldPosition1 = Input.GetTouch(0).position;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                var tempPosition = Input.GetTouch(0).position;
                float diff = tempPosition.x - oldPosition1.x;
                var angel = Quaternion.LookRotation(PlayerTransform.position);
                //if (diff > 0)
                  //  transform.rotation = Quaternion.Slerp(transform.rotation, angel, 5f);
                //else if (diff < 0)
                cameraParent.transform.Rotate(Vector3.forward, diff * 0.1f, Space.Self);

                oldPosition1 = Input.GetTouch(0).position;
            }
        }
        transform.LookAt(PlayerTransform);


    }

    public void switchMode(int t)
    {
        currentMode = t;
    }
    private void Throw()
    {
        if (!gyroEnabled)
            return;
        
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    var tempPosition1 = Input.GetTouch(0).position;
                    var tempPosition2 = Input.GetTouch(1).position;

                    if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                    {
                        if (tempdistance > 0)
                            tempdistance -= 0.5f;
                    }
                    else
                    {
                        if (tempdistance < 100f)
                        {
                            tempdistance += 0.5f;
                        }
                    }
                    oldPosition1 = tempPosition1;
                    oldPosition2 = tempPosition2;
                }
            }

            transform.position = PlayerTransform.position + new Vector3(-tempdistance, 2 + tempdistance, 0);
        transform.localRotation = gyro.attitude * rot;
        Debug.Log(gyro.attitude);
    }
}
