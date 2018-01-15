using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpawnScript : MonoBehaviour {

    public Transform dieSpawner;
    public Transform D6;
    public Transform dieTracker;

    //int dice;
    public int diceAmount = 1;
	// Use this for initialization
	void Start () {
        dieTracker.GetComponent<DicePool>().SetSpawner(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (Input.GetButtonUp("Fire1"))
        {
            SpawnDice(diceAmount);
        }
        */
        
    }

    public void SpawnDice(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            SpawnDie();
        }
    }

    public void SpawnDie()
    {
        Transform die = Instantiate(D6, dieSpawner.position, Random.rotation);
        die.SetParent(dieTracker);
    }

    //todo; make the dice spread out during spawn, maybe make a them form a cube or something?
    // so the colliders don't bug out and send them spraying out everywhere
    // basically 3d tiling? first x, then z, then y? How to organize what is within each layer
}
