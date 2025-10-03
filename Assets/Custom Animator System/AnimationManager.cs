using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[System.Serializable]
public class AnimationManager
{
    public PlayableGraph graph;
    public PlayStack playStack;
    public AnimationClip[] animClips;


    public AnimationManager(Animator animP, CharacterDataSO data)
    {
        SetPlayerAnimClips(data);
        graph = PlayableGraph.Create();

        var playStackPlayable = ScriptPlayable<PlayStack>.Create(graph);
        
        playStack = playStackPlayable.GetBehaviour();

        playStack.Initialize(animClips, playStackPlayable, graph);
        var playableOutput = AnimationPlayableOutput.Create(graph, "Animation", animP);

        playableOutput.SetSourcePlayable(playStackPlayable, 0);

        graph.Play();
    }

    public void SetPlayerAnimClips(CharacterDataSO data)
    {
        animClips = new AnimationClip[data.moveList.Length + data.basicAnimations.Length];
        for (int i = 0; i < data.basicAnimations.Length; i++)
        {
            animClips[i] = data.basicAnimations[i];
        }
        for (int i = 0; i < data.moveList.Length; i++)
        {
            animClips[i + data.basicAnimations.Length] = data.moveList[i].moveAnimation;
        }
    }

    public void PlayAnimation(string animationName)
    {
        playStack.PlayAnimation(animationName);
        //Debug.Log("you have played the animation");
    }

    public void DestroyGraph()
    {
        graph.Destroy();
    }

    public void ShowGraph()
    {
        GraphVisualizerClient.ClearGraphs();
        GraphVisualizerClient.Show(graph);
    }

}
