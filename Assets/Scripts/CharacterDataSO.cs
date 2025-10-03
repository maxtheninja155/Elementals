using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Create New Character")]
public class CharacterDataSO : ScriptableObject
{

    public CharacterRoster character;

    [Header("Character Stats")]
    public float jumpHeight = 50f;
    public float maxSpeed = 7f;
    public AnimationClip[] basicAnimations;
    public Move[] moveList;

}

[System.Serializable]
public class Move
{
    public string moveName;
    public AnimationClip moveAnimation;
    public List<SequenceBlock> inputSequence;

   
    

}

