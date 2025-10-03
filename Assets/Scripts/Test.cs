using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test : MonoBehaviour
{
    public MoveSet set1 = new MoveSet("Water Shaman");

    //string test = "string";

    public Moves projectileMove = new Moves(5f, 7f, 3f, "Hadouken", "down, down-forward, forward, medium punch");
    public Moves heavyMove = new Moves(6f, 10f, 7f, "Smash", "forward, heavy punch");
    public Moves lightMove = new Moves(3f, 5f, 2f, "Jab", "light punch");

    // Start is called before the first frame update
    void Start()
    {
        //projectileMove.PrintMove();
    }

    // Update is called once per frame
    void Update()
    {
    }

}
