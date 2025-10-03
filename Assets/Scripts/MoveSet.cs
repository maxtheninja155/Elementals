using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveSet 
{
    public string character;

    public List<Moves> moveList;

    public MoveSet(string characterP)
    {
        character = characterP;
    }

    public void MoveList()
    {
        Debug.Log("");
    }


}
