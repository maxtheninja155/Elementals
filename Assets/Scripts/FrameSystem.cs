using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime; 
public class FrameSystem : MonoBehaviour
{
    public int frameRate = 60;
    public float frameLength;
    public float currentInterval = 0f;

    public int frameCount;

    void Start()
    {
        frameLength = 1f / frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrames();
    }

    void UpdateFrames()
    {
        currentInterval += Time.deltaTime;
        if(currentInterval >= frameLength)
        {
            ActivateFrame();
        }
    }

    void ActivateFrame()
    {
        currentInterval = 0f;
        frameCount++;
        //Debug.Log("Frame " + frameCount + " played.");
    }
}
