using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    float camSpeed = 20;

    public Transform focus;

    Vector3[] RotPos;
    Vector3 Pos1 = new Vector3(-6.12f, 6.3f, -6);
    Vector3 Pos2 = new Vector3(-6.12f, 6.3f, 6);
    Vector3 Pos3 = new Vector3(6.12f, 6.3f, 6);    
    Vector3 Pos4 = new Vector3(6.12f, 6.3f, -6);
    public int currentRotPos = 0;
    public int previousRotPos;
    float moveTime;

    void Awake()
    {
        RotPos = new Vector3[4] { Pos1, Pos2, Pos3, Pos4};
        focus = GameObject.FindGameObjectWithTag("Player").transform;

        gameObject.transform.position = RotPos[currentRotPos];
    }

    //to do:
    //Add x and z movement
    //probably some ease out/in inertia?
    //focus movement? Might require unit selection first,
    //look into that
	
	// Update is called once per frame
	void Update () {
       
        moveTime = moveTime + Time.deltaTime;

        if (currentRotPos >= 4) { currentRotPos = 0; }
        if (currentRotPos <= -1) { currentRotPos = 3; }

        RotRight();
        RotLeft();
        transform.position = Vector3.Lerp(RotPos[previousRotPos], RotPos[currentRotPos]+focus.position, moveTime);
        transform.LookAt(focus);

    }

    void RotRight()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateRotPos(true);                      
        }
    }

    void RotLeft()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateRotPos(false);
        }
    }

    void UpdateRotPos(bool plussMinus)
    {
        moveTime = 0;
        previousRotPos = currentRotPos;
        currentRotPos = currentRotPos + (plussMinus ? 1 : -1);

    }
}
