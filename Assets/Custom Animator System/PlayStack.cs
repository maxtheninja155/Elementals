using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

public class PlayStack : PlayableBehaviour
{

    private Playable animMixer;
    double clipDuration;

    (AnimationClipPlayable, int) activePlayable;


    public void Initialize(AnimationClip[] moveAnims, Playable owner, PlayableGraph graph)
    {

        owner.SetInputCount(1);
        animMixer = AnimationMixerPlayable.Create(graph, moveAnims.Length);
        graph.Connect(animMixer, 0, owner, 0);

        owner.SetInputWeight(0, 1f);

        for (int clipIndex = 0; clipIndex < animMixer.GetInputCount(); clipIndex++)
        {
            graph.Connect(AnimationClipPlayable.Create(graph, moveAnims[clipIndex]), 0, animMixer, clipIndex);
            animMixer.SetInputWeight(clipIndex, 0f);
        }

        animMixer.SetInputWeight(0, 1f);

        activePlayable = ((AnimationClipPlayable)animMixer.GetInput(0), 0);
        
    }

    public void PlayAnimation(string animationName)
    {
        for (int i = 0; i < animMixer.GetInputCount(); i++) //go through all the animation clip playables in the graph
        {
            AnimationClipPlayable nextPlayable = (AnimationClipPlayable)animMixer.GetInput(i);

            
            if (nextPlayable.GetAnimationClip().name == animationName)
            {
                animMixer.SetInputWeight(activePlayable.Item2, 0f); // step one, set old active to zero
                animMixer.SetInputWeight(nextPlayable, 1f);// step two, set new animation to 1
                clipDuration = nextPlayable.GetAnimationClip().length;
                nextPlayable.SetTime(0);
                //Debug.Log("animation is played");

                activePlayable = (nextPlayable, i); // step three, store new animation as active
            }
        }
    }

    public void MoveAnimation(string animationName)
    {

    }


    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if(animMixer.GetInputCount() == 0)
        {
            Debug.LogAssertion("Animation Mixer has Zero inputs"); 
            return;
        }

        if (!activePlayable.Item1.GetAnimationClip().isLooping)
        {
            clipDuration -= info.deltaTime;
        }

        // If the clip duration is over and the clip does not loop, eg: running animation.
        // then set idle playable weight to 1 and set active playble back to idle.
        if (clipDuration <= 0 && !activePlayable.Item1.GetAnimationClip().isLooping)
        {
            animMixer.SetInputWeight(activePlayable.Item2, 0f);
            animMixer.SetInputWeight(0, 1f);
            activePlayable = ((AnimationClipPlayable)animMixer.GetInput(0), 0);
        }
    }

}
