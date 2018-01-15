using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePool : MonoBehaviour {

    //to do
    //write a bit in the die script to prompt recount in DicePool
    //


    
    
    


    List<Transform> diceList;

    Transform[] diceArray;

    Transform diePool;
    

    public int resultTotalCurrent;
    int resultPresentation = 0;

    DieSpawnScript dieSpawner;

    bool throwResolved = false;

    Transform result;

    void Awake()
    {
        diePool = gameObject.transform;
        result = GameObject.Find("Result").GetComponent<RectTransform>();
        diceList = new List<Transform>();
    }


    public void SetSpawner(DieSpawnScript spawner)
    {
        dieSpawner = spawner;
    }

    void Update()
    {
        TrackDice();

        GradualIncreaseDestroyDiceArray();
    }



    void TrackDice()
    {
        
        if (diePool.childCount > 0)
        {

            GetCurrentDiceInPoolArray();

            if (HaveTheDiceResolved())
            {

                GetResult();
                PrintResult();

            }
        }


        
    }

    bool countingIsGo = false;

    void GetCurrentDiceInPoolArray()
    {

        diceArray = new Transform[diePool.childCount];
            
            for (int i = 0; i < diePool.childCount; i++)
            {
                diceArray[i] = diePool.GetChild(i);
            }
        
            
    }

    bool HaveTheDiceResolved()
    {
        int unresolvedDice = 0;

        bool atLeastOneDieHasNotResolved = false;

        for (int i = 0; i < diePool.childCount; i++)
        {
            GetCurrentDiceInPoolArray();
            //Debug.Log(dice.Length +"/"+ i +" pidgeons fucking up its house.");
            if (!diceArray[i].GetComponent<Die>().resolved) { atLeastOneDieHasNotResolved = true; unresolvedDice++; }
            if (diceArray[i].transform.position.y < 0) { Debug.Log("Die out of bounds, spawning new."); Destroy(diceArray[i].gameObject); dieSpawner.SpawnDie(); }
        }

        //Debug.Log("There are currently "+unresolvedDice+" unresolved dice.");


        return !atLeastOneDieHasNotResolved;
    }

    
    

    void GetResult()
    {
        int currentResultTotal = 0;

        //getcomponent in children; die.result
        

        currentResultTotal = SetResultInstant(currentResultTotal);

        resultTotalCurrent = currentResultTotal;

        //spawn a number facing the camera = total of dice, position: average xz of dice in dietracker

    }

    bool stillCounting = false;


   

    private int SetResultInstant(int currentResultTotal)
    {
        for (int i = 0; i < diePool.childCount; i++)
        {
            currentResultTotal = currentResultTotal + diceArray[i].GetComponent<Die>().result;
        }

        return currentResultTotal;
    }


    public bool resultInstant = false;
    

    void PrintResult()
    {

        //Debug.Log("There are currently "+diePool.childCount+" dice in the pool.");

        //Debug.Log("The result is " + resultTotalCurrent);

        switch (resultInstant)
        {
            case true:
                result.GetComponent<Text>().text = resultTotalCurrent.ToString();
                break;
            case false:
                SetResultGradual();
                break;
            default:
                Debug.Log("wat.");
                break;
        }

        

        /*
        if (resultPresentation < resultTotalCurrent)
        {
            resultPresentation++;
            result.GetComponent<Text>().text =  resultPresentation.ToString(); 
        }
        /*
        RectTransform resultBoard = GameObject.Find("ResultCanvas").GetComponent<RectTransform>();        
        resultBoard.anchoredPosition.x = diePool.position.x;
        */
        
    }

    int resultGradual = 0;

    public int ResetResultGradual() { return resultGradual = 0; }

    private void SetResultGradual()
    {
        if (resultPresentation < resultTotalCurrent)
        {
            //Debug.Log(dice.Length + " is tired of these " + resultGradual + " snakes on this motherfucking plane.");

            resultPresentation = resultPresentation + diceArray[resultGradual].GetComponent<Die>().result;
            
            result.GetComponent<Text>().text = resultPresentation.ToString();

            SpawnDieResultNumber(resultGradual, diceArray[resultGradual].GetComponent<Die>().result);

            resultGradual++;
        }

    }


    public Canvas resultCanvas;
    public Transform dieResultPresentation;

    void SpawnDieResultNumber(int die, int dieResult)
    {

        //Debug.Log(dieResult);


        Transform currentThin = Instantiate(dieResultPresentation, diceArray[die].transform.position + new Vector3(0, 2, 0), Quaternion.Euler(0,45,0));
        currentThin.GetComponent<Text>().text = dieResult.ToString();
        currentThin.SetParent(resultCanvas.transform.Find("IndividualResults"));
        
        
    }

    public void RemoveDiceInstant()
    {
        

        for(int i = 0; i < diceArray.Length; i++)
        {
            Destroy(diceArray[i].gameObject);
            Destroy(resultCanvas.transform.Find("IndividualResults").GetChild(i).gameObject);
        }
    }

    int gradualIncrease = 0;
    bool gradualRemove = false;

    void GradualIncreaseDestroyDiceArray()
    {
        

        if (gradualRemove)
        {                      
            //gradualIncrease++;
            if(diceArray[gradualIncrease] == null) { gradualRemove = false;
                result.GetComponent<Text>().text = " "; }
            if (gradualRemove) {
                GetCurrentDiceInPoolArray();
                Destroy(diceArray[gradualIncrease].gameObject);
                Destroy(resultCanvas.transform.Find("IndividualResults").GetChild(gradualIncrease).gameObject); }

            Debug.Log("Gradual is " + gradualRemove);

        }
    }

    public void RemoveDiceGradualArray()
    {
        gradualIncrease = 0;
        gradualRemove = true;        
    }






}
