using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class s_Die : MonoBehaviour {

    

    public DieType dieType;

    Rigidbody rb;

    public RectTransform[] sides;
    public Text[] sideNumbers;
    public string[] sideNumberString;
    public s_GameMaster gameMaster;

    public int result;
    bool resolved = false;

    public Text statDrag;

    void Setup(s_GameMaster gm) {

        gameMaster = gm;

    }


	
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        //statDrag = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        //Debug.Log(sides[0].GetComponent<Text>().text);
        //need t bring in the getcomponents here. optimization!
        sideNumbers = new Text[sides.Length];
        //sideNumberString = new string[sides.Length];
        for(int i = 0; i < sides.Length; i++){
            
            sideNumbers[i] = sides[i].GetComponent<Text>();
            //Debug.Log(""+ sides[i].GetComponent<Text>().text+", "+ sideNumbers[i].text);
            //sideNumberString[i] = sides[i].GetComponent<Text>().text;    
        } //for some reason this isn't working. Why? D: yknowhat, I'll just comment that shit out and place the text components in the array manually. Fuck it.
        //wait, I'll try just changing the textcomponent instead of the string-part of the text component. Maybe that will work?
        
    }
	
	
	void Update () {
        if (!held)rb.drag = 0f;
        //Debug.Log(gameObject.name + ": " + held + ", " + rb.drag);

        statDrag.text = rb.drag.ToString() + " & " + restTimeInSeconds.ToString() + "\n" + result;

        held = false;
	}

    void LateUpdate(){
        if(!IsRolling() && !held)
        {
            restTimeInSeconds = restTimeInSeconds + Time.deltaTime;

            if(restTimeInSeconds >= restTimeThreshold && !resolved)
            {           
              Resolve();   
            }
        }
    
    }



//RESOLUTION SECTION

    float restTimeInSeconds;
    public float restTimeThreshold = 0.5f;

    bool IsRolling()
    {
        if (rb.velocity == new Vector3(0,0,0)){return false;}
        else {restTimeInSeconds = 0; return true;}
        
    }

    bool Cocked(){

        bool isItCocked = false;


        switch(dieType){
            case DieType.d6:
                
                int totalSidesThatAreAtTheSameLevel = 0;
                //1 & 6
                //2 & 5
                //3 & 4

                //check if the y value is the same on at least two
                Debug.Log("1 is "+ sides[0].position.y + " and 6 is " + sides[5].position.y);
                Debug.Log("2 is "+ sides[1].position.y + " and 5 is " + sides[4].position.y);
                Debug.Log("3 is "+ sides[2].position.y + " and 4 is " + sides[3].position.y);


                if(sides[0].position.y == sides[5].position.y)
                {

            
                    totalSidesThatAreAtTheSameLevel++;
                }

                if(sides[1].position.y == sides[4].position.y){
                    totalSidesThatAreAtTheSameLevel++;
                }

                if(sides[2].position.y == sides[3].position.y){
                    totalSidesThatAreAtTheSameLevel++;
                }
                    Debug.Log("A total of "+totalSidesThatAreAtTheSameLevel+" pairs of sides are at the same height.");
                if(totalSidesThatAreAtTheSameLevel >= 2){
                    Debug.Log("This die roll is acceptable!");
                    isItCocked = false;
                } else if (totalSidesThatAreAtTheSameLevel < 2){
                    Debug.Log("This die is Swedish!");
                    isItCocked = true;
                }
                break;
            
                

            default:
                break;
        }

        

        return isItCocked;
    }

    RectTransform currentTop; //current top side

    void ResultGet()
    {
        //first get which side is top
        currentTop = sides[0];
        int numberIndex = 0;
        for(int i = 0; i < sides.Length; i++){
           if( sides[i].position.y >= currentTop.position.y){
                currentTop = sides[i];
                numberIndex = i;
           } 
        }
        //then get which value that side has
        int.TryParse(sideNumbers[numberIndex].text, out result);

    }

    
    void Resolve()
    {
        //write method to check if die is lying flat on the group (one pair of two sides must have the same xz coordinates)
        //if return true, resultget
        if(!Cocked()){ResultGet();}
        

        

        //after resolving the result of the die roll, set resolved to true so it won't spam
        resolved = true;
        Debug.Log("The roll has resolved as: "+ result);
        if(result > 0){ResultSend();}
        
    }


    void ResultSend()
    {
        Debug.Log("Sending the result '"+ result+ "' to the GameMaster");
        //gameMaster.AddResult(result);
    }





//HANDLING SECTION


    public void GatherUp(Vector3 hand, float power)
    {

        
        Vector3 direction = (gameObject.transform.position + hand) *-1;
        rb.AddForce(direction * power * Time.deltaTime);
        resolved = false;

    }

    float startTime;
    float speed = 2;
    bool held = false;



    public void GatherUp(Vector3 hand)
    {


        
        Vector3 direction = hand - gameObject.transform.position;
        rb.AddForce(direction * 250 * Time.deltaTime);
        rb.drag = 10/ Vector3.Distance(gameObject.transform.position, hand);
        Debug.DrawRay(gameObject.transform.position, direction, Color.blue, 0f, true);
        held = true;
        resolved = false;

        if(Input.GetKey(KeyCode.R)||Input.GetMouseButton(0)){
            RandomRot();
        }

        //SemiRandomRot(hand);
        

        /*
        float travel = Vector3.Distance(gameObject.transform.position, hand);
        float cov = (Time.time - startTime) * speed;
        float frac = cov / travel;

        Vector3.Lerp(gameObject.transform.position, hand, frac);

        Debug.Log("Gathering");*/

    }

    public void RandomRot(){
        gameObject.transform.rotation = Random.rotation;
    }

    float semiRandomRotCountDownReset = 100;
    float semiRandomRotCountDown = 100;
    void SemiRandomRot(Vector3 hand) //with this I will make the die run a randomRot if it Held == true && velocity over a certain parameter where the
    {
        if(!held){
            semiRandomRotCountDown = semiRandomRotCountDownReset;
            return;
        }

        if(held){
            semiRandomRotCountDown = semiRandomRotCountDown / Vector3.Distance(gameObject.transform.position, hand);
            semiRandomRotCountDown = semiRandomRotCountDown - 1;
            if(semiRandomRotCountDown <= 0){
                RandomRot();
            }
        }
    }

//CLEANUP SECTION
    public void Seppuku()
    {
        Destroy(gameObject);
    }
}
