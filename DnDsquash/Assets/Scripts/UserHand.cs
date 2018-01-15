using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour {

    public Camera gameCam;

    public float range = 500f;
	
	void Start () {
		
	}

    /*
    public GameObject particle;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
                Instantiate(particle, transform.position + new Vector3(0,5,0), transform.rotation);
        }
    }*/
    /*
	void Update () {
        //raycast from camera, then move 'hand' to xz coordinates, y +10?
        RaycastHit hit;
        if (Physics.Raycast(gameCam.transform.position, gameCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.position);
        }

	}*/
}
