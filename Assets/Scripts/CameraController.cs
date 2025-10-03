using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CameraController : MonoBehaviour
{
    public Transform rightLim;
    public Transform leftLim;
    public Transform zMax;
    public Transform zMin;
    public float zMultiplier;

    public float camHeight;
    public float zoomDistance;

    public static float distanceBetweenPlayers;
    public static float rawDistance;
    public float rawDistWatcher;

    public static float maxHeightofPlayers;
    public static float zDistance;
    public float midpoint;
    public GameObject midpointGO;
    public float midpointY;

    public static float maxXDistance = 6.5f;

    public static bool flipP1 = false;
    public static bool flipP2 = false;
   
    public GameObject player1;
    public GameObject player2;

    public float currentDistance;

    

    void Awake()
    {
        rawDistance = -1;
    }



    void Update()
    {

        CheckPlayerFlip();
        rawDistance = player1.transform.position.x - player2.transform.position.x;
        distanceBetweenPlayers = Mathf.Abs(rawDistance);

        currentDistance = distanceBetweenPlayers;
        CalculateMidpointY();
        CalculateMidpointX();
        CameraTracking();
        CameraZoom();
        rawDistWatcher = rawDistance;

    }

    public void CheckPlayerFlip()
    {
        if (Math.Sign(player1.transform.position.x - player2.transform.position.x) != Math.Sign(rawDistance))
        {
            flipP1 = true;
            flipP2 = true;
            Debug.Log("switched");
        }
    }


    public void CalculateMidpointX()
    {
        midpoint = (player1.transform.position.x + player2.transform.position.x) / 2f;
        midpointGO.transform.position = new Vector3(midpoint, midpointY + 1.51f, 0.1f);
    }

    public void CalculateMidpointY()
    {
        midpointY = (player1.transform.position.y + player2.transform.position.y) / 2f;
    }

    public void CameraTracking()
    {
        transform.position = new Vector3(midpoint, CameraHeightTracking(), -4.67f);

        //wall clamping
        if(transform.position.x <= leftLim.transform.position.x)
        {
            transform.position = new Vector3(leftLim.transform.position.x, transform.position.y, transform.position.z);
        }
        if(transform.position.x >= rightLim.transform.position.x)
        {
            transform.position = new Vector3(rightLim.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public float CameraHeightTracking()
    {
        if(midpointY <= 7.5f)
        {
            return 7.62f;
        }
        else
        {
            return midpointY + camHeight;
        }
    }

    public void CameraZoom()
    {
        //zooming in and out
        zMultiplier = distanceBetweenPlayers;
        transform.position = new Vector3(transform.position.x, transform.position.y, -zMultiplier);
        
        if(transform.position.z >= zMin.transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin.transform.position.z); 
        }
        if (transform.position.z <= zMax.transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax.transform.position.z);
        }
    }
}
