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
        ResultGet();

        

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
        
        /*
        float travel = Vector3.Distance(gameObject.transform.position, hand);
        float cov = (Time.time - startTime) * speed;
        float frac = cov / travel;

        Vector3.Lerp(gameObject.transform.position, hand, frac);

        Debug.Log("Gathering");*/

    }

//CLEANUP SECTION
    public void Seppuku()
    {
        Destroy(gameObject);
    }
}
