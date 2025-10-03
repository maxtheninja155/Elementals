using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Moves 
{
    //public AnimationClip anim;

    public float startUpFrames;
    public float activeFrames;
    public float endFrames;

    public string smName;

    public string buttonOrder;


    public Moves( float suFP, float aFP, float eFP, string smNameP, string buttonComboP)
    {
        //anim = animP;
        startUpFrames = suFP;
        activeFrames = aFP;
        endFrames = eFP;
        smName = smNameP;
        buttonOrder = buttonComboP;

    }

    public void PrintMove()
    {
        Debug.Log("you just hit him with a " + smName + ". You took " + startUpFrames + " frams to start up, "
            + activeFrames + " frames to hit him, and had to wait " + endFrames + 
            " frames for your move to end! The input you performed to exacute this was "
            + buttonOrder + ". GOOD JOB!");
    }


}
