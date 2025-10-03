using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonSpawner : MonoBehaviour
{

    public float timeBetweemSpawns;
    public float spawnDistance;
    public Nucleon[] nucleonPrefabs;

    float timeSinceLastSpawn;

    public bool isSpawningAtoms = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
            isSpawningAtoms = true;
        if (Input.GetKeyUp(KeyCode.P))
            isSpawningAtoms = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        /*timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= timeBetweemSpawns)
        {
            timeSinceLastSpawn -= timeBetweemSpawns;

            SpawnNucleon();
    
        }
        */
        if (isSpawningAtoms)
        {
            SpawnNucleon();
        }
            


    }

    void SpawnNucleon()
    {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }
}
