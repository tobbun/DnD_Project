using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_User : MonoBehaviour {


    

    public Transform hand;
    public Transform myHand;
    s_handControl handControl;

    public UserType thisUserIsA;

    // Use this for initialization
    void Start () {
		
        if(myHand == null)
        {
            myHand = Instantiate(hand);
        }

        handControl = myHand.GetComponent<s_handControl>();

        //handControl.Setup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Check (int DC, DieType die)
    {

    }
    


}
