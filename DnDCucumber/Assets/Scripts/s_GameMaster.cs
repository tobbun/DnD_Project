using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_GameMaster : MonoBehaviour {


    

    //prefabs
    public Transform die;




    //lists of things
    

    public List<int> resultsCurrent, resultsTemp;


    void EarlyUpdate() //upkeep
    {

    }


	void Update () //mainphase
    {
        if (Input.GetButton("Fire1"))
        {
            
        }



	}



    void LateUpdate() //endstep
    {

        



    }


    void CollectDice()
    {
        //when button is pressed, all active dice in scene are collected near the mouse point.
    }

    public void AddResult(int result)
    {

    }

    void TransferResult()
    {
        if (resultsTemp.Count < 0)
        {
            resultsCurrent.Clear();

            for (int i = 0; i < resultsTemp.Count; i++)
            {
                resultsCurrent.Add(resultsTemp[i]);
            }

        }
    }

    void SpawnDie()
    {

        


    }

    public class Dice
    {

        public List<Transform> dice;

        public Dice()
        {

        }

        public void Gather()
        {

        }
        
    }
}


//global shit?
public enum DieType { d4, d6, d8, d10, d12, d20 }
public enum UserType { Player, GameMaster, Observer, AI}
