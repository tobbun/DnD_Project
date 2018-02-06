using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_handControl : MonoBehaviour {

    Camera cam;

    public float speed = 10;
    Vector3 pos;

    public List<GameObject> dice;

    public s_User user;

    public Slider diceToSpawn;

    public Toggle shallWeCubeTheFuckers;
    public Toggle rotOrNot;

    public Text howManyDice;

    public Transform handPoint;

    public Transform spawnPoint;
    public GameObject die;

    public GameObject[] dieType;

    public Text DieCount;

    void Start ()
    {

        cam = Camera.main;
        pos = transform.position;
        dice.Capacity = 1000;
        //Setup();



    }

    public void Setup(s_User newUser)
    {
        user = newUser;
        dice.Add(GameObject.FindGameObjectWithTag("D6"));

    }

    public void Setup()
    {
        

        dice.Add(GameObject.FindGameObjectWithTag("D6"));
    }

	void Update () {
        /*Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
            //gameObject.transform.Translate();
        }*/
        

        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            transform.position = hit.point;
        }


        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SpawnD6(1);
        }

        DieCount.text = "You have "+dice.Count+" dice.";

        howManyDice.text = " "+ diceToSpawn.value;
        
    }   


    void LateUpdate()
    {
        Gather();
    }

// SPAWNING SECTION
    void SpawnAsCube(Transform SpawnPoint, GameObject die){

        int axisLength = FindTheCubeRoot(diceToSpawn.value);
        
        /* 
        int yAxisLength = 5;
        int xAxisLength = 5;
        int zAxisLength = 5;
        */

        float spacing = 1;
        int diceAlreadySpawned = 0;

        for(int i = 0; i < axisLength; i++){
            for(int p = 0; p < axisLength; p++){
                for(int r = 0; r < axisLength; r++){
                    Vector3 relativeSpawnPoint = new Vector3(i*spacing,p*spacing,r*spacing);
                    if(diceAlreadySpawned < diceToSpawn.value){
                        if(rotOrNot.isOn){rot = Random.rotation;}
                        dice.Add(Instantiate(die, SpawnPoint.position+relativeSpawnPoint, rot));
                        diceAlreadySpawned++;
                    }
                }
            }   
        }
    }

    int FindTheCubeRoot(float valueToCube){

        double root = (System.Math.Pow((double)valueToCube, (1.0/3.0)));


        return Mathf.CeilToInt((float)root);
    }


    public void SpawnD4(int dieIndex)
    {
        rot = new Quaternion (0,0,0,1);
        
        

        if(shallWeCubeTheFuckers.isOn){
            SpawnAsCube(spawnPoint, dieType[dieIndex]);
        }
        else {
        
            for (int i = 0; i < diceToSpawn.value; i++)
            {
                if(rotOrNot.isOn){rot = Random.rotation;}
                dice.Add(Instantiate(dieType[dieIndex], spawnPoint.position, rot));
                //make list of s_die components?
            }
        
        }
    }

    Quaternion rot;
    public void SpawnD6(int dieIndex)
    {

        rot = new Quaternion (0,0,0,1);
        
        

        if(shallWeCubeTheFuckers.isOn){
            SpawnAsCube(spawnPoint, dieType[dieIndex]);
        }
        else {
        
            for (int i = 0; i < diceToSpawn.value; i++)
            {
                if(rotOrNot.isOn){rot = Random.rotation;}
                dice.Add(Instantiate(dieType[dieIndex], spawnPoint.position, rot));
                //make list of s_die components?
            }
        
        }
    }

    public void SpawnD8(int dieIndex)
    {
        Debug.Log("Die type not yet implemented.");
    }

    public void SpawnD10(int dieIndex)
    {
        Debug.Log("Die type not yet implemented.");
    }

    public void SpawnD12(int dieIndex)
    {
       Debug.Log("Die type not yet implemented.");     
    }

    public void SpawnD20(int dieIndex)
    {
       rot = new Quaternion (0,0,0,1);
        
        

        if(shallWeCubeTheFuckers.isOn){
            SpawnAsCube(spawnPoint, dieType[dieIndex]);
        }
        else {
        
            for (int i = 0; i < diceToSpawn.value; i++)
            {
                if(rotOrNot.isOn){rot = Random.rotation;}
                dice.Add(Instantiate(dieType[dieIndex], spawnPoint.position, rot));
                //make list of s_die components?
            }
        
        }
    }

/*
    public void SpawnDie()
    {
        SpawnD6(handPoint);
    }
*/

//HANDLING

    void Gather(){
        if (Input.GetMouseButton(1))
        {
            foreach (GameObject Die in dice)
            {
                
                /*
                rbDie = Die.GetComponent<Rigidbody>();

                Vector3 direction = (Die.transform.position + handPoint.transform.position);
                rbDie.AddForce(direction * speed * Time.deltaTime);
                */
                Die.GetComponent<s_Die>().GatherUp(handPoint.transform.position);

            }
        }
    }

//CLEANUP SECTION
    public void RemoveDice()
    {
        foreach (GameObject Die in dice)
        {
            
            Die.GetComponent<s_Die>().Seppuku();
            
        }

        dice.Clear();
    }
}

public class DieSpawner
{

    List<GameObject> xList;

    //TODO: Somehow manage to make a List of Lists?

    public DieSpawner()
    {

    }

    public DieSpawner(int dieAmount)
    {

    }

    void SpawnDie1D(int amount)
    {

    }

    void SpawnDie2D(int amount)
    {

    }

    void SpawnDie3D(int amount)
    {

    }

    void Xlist()
    {

    }

    void Ylist()
    {

    }

    void Zlist()
    {

    }

}
