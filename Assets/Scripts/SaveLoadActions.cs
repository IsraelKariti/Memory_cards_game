using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadActions : MonoBehaviour
{
    private readonly string TAG = "MoonActive";

    public SaveLoadWrapper saveLoadWrapper;
    public GameObject deck;
    public TimerScript timer;
    public GameScript gameScript;

    private ISaveLoad saveLoad;
    void Start()
    {
        saveLoad = saveLoadWrapper.GetSaveLoad();
    }

    public void saveGameToMem()
    {
        Debug.Log(TAG + " SaveLoadActions saveGameToMem");
        // save the cards
        Debug.Log(TAG + " call SaveAllCardsLoctionsToMemory");

        deck.GetComponent<AllCardsScript>().SaveAllCardsLoctionsToMemory();
        Debug.Log(TAG + " call SaveAllCardsActivenessToMemory");

        deck.GetComponent<AllCardsScript>().SaveAllCardsActivenessToMemory();

        // save the clock
        Debug.Log(TAG + " call SaveTimerToMemory");

        timer.SaveTimerToMemory();
        // save the number of undiscovered couples
        Debug.Log(TAG + " call GetNumberOfUndiscoveredCouples");

        saveLoad.SetInt("undiscoveredCouples", gameScript.GetNumberOfUndiscoveredCouples());

        gameScript.SaveLevelNumber();
        // set the flag so that Menu Scene will know there is a game saved
        PlayerPrefs.SetInt("hasGameBeenSaved", 1);
        Debug.Log(TAG + " call FinishTheSavedGame");


        gameScript.FinishTheSavedGame();
    }

    public void FetchGameFromMem()
    {
        ISaveLoad saveLoad = saveLoadWrapper.GetSaveLoad();
        int[] cardIndecisInTheLocationArray = new int[16];
        bool[] cardActiveness = new bool[16];

        for (int j = 0; j < 16; j++)
        {
            cardIndecisInTheLocationArray[j] = saveLoad.GetInt("cardLocation" + j);
            cardActiveness[j] = saveLoad.GetBool("cardActiveness" + j);
            Debug.Log(TAG + "LOAD card" + j + " position: " + cardIndecisInTheLocationArray[j] + " active "+cardActiveness[j]);

        }
    }
}
