using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    enum gameState {play, pause, pauseResolve};

    public Transform tile;

    public Transform dieSpawner;

    public Slider diceAmount;

    public Toggle instantResolution;

    public int dice;

    public DicePool dieTracker;

    /*
     Single turn:
     - Determine Start Step effects (things that resolve at start of turn)
        - 2 types of priority
            - Additive (descending order of added effects)
            - Mend then Rend (healing effects first, then damaging effects)
     - Player Choice Phase
     - Determine End Step effects (things that resolve at end of turn)
         
         */

    /* 
    Round management universal bits:
    - Players (list or array of players?)
    - Current player
    - Current turn
    - Current phase
    - Dice (how many?)
      - Spawn dice? Hold lmb to gather dice around cursor, press rmb to spawn more? (shift+rmb to despawn dice?)
      - Dice held over raycast to terrain? or something? (moveing them moves them floating over terrain?
      - Die size? Press "," and "." to enlarge/reduce?
      - 
        */
    public void Round()
    {

    }


    public void callCheckRoll() //sets the state of the game to "paused", calls up relevant button OR spawns relevant dice hovering around cursor
    {
        // set game state to pauseResolve
        dice = Mathf.RoundToInt(diceAmount.value);
        dieSpawner.GetComponent<DieSpawnScript>().SpawnDice(dice);
        // 
    }

    public void resetDice()
    {
        dieTracker.ResetResultGradual();

        switch (instantResolution.isOn)
        {
            case true: dieTracker.RemoveDiceInstant();
                break;
            case false: dieTracker.RemoveDiceGradualArray();
                break;
            default:
                print("What.");
                break;
        }        
    }

    //Level generation

    void GenerateLevel(int length, int width)
    {
        // two arrays, both alike in dignity
        // two for loops; one for length, one for width
        // this kills the man

        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        // make an array of the tiles in the level?
        // have each tile store xz coordinates? Add tiles with +- 1 coordinate to neighbours array?
        // kill myself?
    }

}
