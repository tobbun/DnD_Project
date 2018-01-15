using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

    public Transform topFace;

    public Transform[] Sides;

    public int result;

    public float restTimeInSeconds;

    public float restThreshold;

    public Vector3 v;

    public AudioSource ass;

    public enum DieType { d4, d6, d8, d10, d12, d20};

    public DieType dieType;

    //to do: somehow get reference to all the children
    // transform reference
    // access rigidbody.velocity
    //setup timer for 1 or .5 seconds
    // timer counts down/up when velocity is 0
    //when still == true, get all children transforms
    // compare y values, get name of child, that is result

    // then later test how that holds up with performance

    Rigidbody rb;

    bool isRolling; 
        
    public bool resolved;

    void Awake()
    {

        rb = gameObject.GetComponent<Rigidbody>();

        ass = gameObject.GetComponent<AudioSource>();

        topFace = Sides[0];

        resolved = false;
    }

    void Update()
    {

        FacePosition();

        v = rb.velocity;

        if (!IsRolling())
        {
            //start timer to count down .5 to 1 seconds

            restTimeInSeconds = restTimeInSeconds + Time.deltaTime;
            
            if(restTimeInSeconds >= restThreshold)
            {
                topFace.GetComponent<TextMesh>().color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time, 0.2f));
                int.TryParse(topFace.name, out result);
                gameObject.name = ""+dieType+"("+result+")";
                resolved = true;
            }
            
                           
        }

        //if (transform.position.y < 0) { }
    }

    private void FacePosition()
    {        
        for (int i = 0; i < Sides.Length; i++)
        {
            if (Sides[i].transform.position.y >= topFace.transform.position.y)
            {
                topFace = Sides[i];
            }
        }
    }

    bool IsRolling()
    {

        if (rb.velocity == new Vector3(0, 0, 0)) { return false; }

        else

        {
            restTimeInSeconds = 0;
            return true; }
    }

    void OnCollisionEnter(Collision col)
    {
        ass.volume = rb.velocity.y;
        ass.Play();
    }

}
