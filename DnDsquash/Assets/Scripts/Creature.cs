using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public Vector2 gridPosition;

    public string creatureName = "Creature";

    public int strength = 10;
    public int dexterity = 10;
    public int constitution = 10;
    public int intelligence = 10;    
    public int wisdom = 10;
    public int charisma = 10;


    public int health;    
    public int vitality;   

    public int movementLand = 30;
    public int movementFly = 0;
    public int movementBurrow = 0;


    public bool action;
    public bool actionBonus;

    public Vector3 start;
    public Vector3 end;
    float time = 0;
    bool moving = false;


    public PlayerClass[] classes;


    //to do; get tile xy as start and end. Current solution unstable

    

    void Start()
    {
        start = gameObject.transform.position;
        end = new Vector3(0,0.8f,0);

        //classes[0] = new PlayerClass();

    }

    void Update()
    {
       time = time + Time.deltaTime * 10;
              
       transform.position = Vector3.Lerp(start, end, time);

        if (time < 1) { moving = true; } else { moving = false; }
    }
    // as long as time < end, bool moving = true, else false

    public void MoveNorth()
    {
        Vector3 North = new Vector3(0, 0, 1);
        //transform.Translate(new Vector3(0,0,1));
        Move(North);
    }

    public void MoveSouth()
    {
        Vector3 South = new Vector3(0, 0, -1);
        //transform.Translate(new Vector3(0, 0, -1));
        Move(South);
    }

    public void MoveEast()
    {
        Vector3 East = new Vector3(1, 0, 0);
        //transform.Translate(new Vector3(1, 0, 0));
        Move(East);
    }

    public void MoveWest()
    {
        Vector3 West = new Vector3(-1, 0, 0);
        //transform.Translate(new Vector3(-1, 0, 0));
        
         Move(West);
    }

    void Move(Vector3 direction)
    {
       
            
        if (!moving)
        {
            start = gameObject.transform.position;
            end = gameObject.transform.position + direction;
            time = 0;
        }
        else
            Debug.Log(creatureName + " is already moving");
       
    }


    //class for actions?
    // variables: Name, image(?), options? (if attack, what attacks, etc)
    // 
}
