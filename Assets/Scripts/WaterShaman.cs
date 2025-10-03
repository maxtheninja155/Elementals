using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShaman : CharacterController 
{
    public GameObject hadoukenPrefab;
    
    public bool hadouken = false;

    public float projSpeedMultiplier = 50f;

    public Transform hadoukenSpawn;

    public void SpecialMovesInputs()
    {

        //if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        if (Input.GetKeyDown(KeyCode.H))
            hadouken = true;
    }
    
    
    public void SpecialMovesUpdate()
    {

        if (hadouken)
        {
            GameObject proj = Instantiate(hadoukenPrefab, hadoukenSpawn.position, Quaternion.identity);
            proj.GetComponent<Rigidbody>().AddForce(Vector3.right * projSpeedMultiplier, ForceMode.Impulse);
            hadouken = false;
        }

    }


}
