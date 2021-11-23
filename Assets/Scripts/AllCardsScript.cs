using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCardsScript : MonoBehaviour
{
    private readonly string TAG = "MoonActive";
    public SaveLoadWrapper saveLoadWrapper;
    public GameObject deck;
    public GameObject card0;
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    public GameObject card6;
    public GameObject card7;
    public GameObject card8;
    public GameObject card9;
    public GameObject card10;
    public GameObject card11;
    public GameObject card12;
    public GameObject card13;
    public GameObject card14;
    public GameObject card15;

    private GameObject[] allCards;
    private Vector3[] locations;
    private int[] cardPositions;// the position of each card in the locations array
    private bool[] occupied;
    private bool[] activatedCards;
    private ISaveLoad saveLoad;
    private void Awake()
    {
        

        saveLoad = saveLoadWrapper.GetSaveLoad();
        
        cardPositions = new int[16];
        locations = new Vector3[] {new Vector3(-2,3,0), new Vector3(-0.666f, 3, 0), new Vector3(0.666f, 3, 0), new Vector3(2, 3, 0),
            new Vector3(-2,1,0), new Vector3(-0.666f, 1, 0), new Vector3(0.666f, 1, 0), new Vector3(2, 1, 0),
            new Vector3(-2,-1,0), new Vector3(-0.666f, -1, 0), new Vector3(0.666f, -1, 0), new Vector3(2, -1, 0),
            new Vector3(-2,-3,0), new Vector3(-0.666f, -3, 0), new Vector3(0.666f, -3, 0), new Vector3(2, -3, 0)
        };
        allCards = new GameObject[] { card0, card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11, card12, card13, card14, card15 };
        activatedCards = new bool[16];
    }
    public void setAllCardsRandomly()
    {

        occupied = new bool[16];
        

        for (int i = 0;i < 16;i++)
        {
            activatedCards[i] = true;
        }
        // set randomly all the cards from 0 to 15

        setAllCardsPotionsRandomly();

        setAllCardsToTheirDecidedPositions();
    }

    

    private void setAllCardsPotionsRandomly()
    {

        for (int j = 0; j < 16; j++)
        {
            //set location of card j
            int r;
            do
            {
                r = Random.Range(0, 16);
            }
            while (occupied[r] == true);

            // now i found a value that is uniqe so far
            // so card No. j is location in spot No. r
            occupied[r] = true;
            cardPositions[j] = r;

        }
    }
    private void setAllCardsToTheirDecidedPositions()
    {
        for (int j = 0; j < 16; j++)
        {
            allCards[j].transform.localPosition = locations[cardPositions[j]];

        }
    }
    public void setAllCardsToAcitve()
    {
        foreach (GameObject go in allCards)
            go.SetActive(true);

        for (int i = 0; i < 16; i++)
        {
            activatedCards[i] = true;
        }
    }

    public int[] GetAllCardIndecisInTheLocationArray()
    {
        return cardPositions;
    }
    public void SetAllCardIndecisInTheLocationArray(int[] positions)
    {

        cardPositions = positions;
    }
    public bool[] GetCardsActiveness()
    {
        return activatedCards;
    }
    public void SetCardsActiveness(bool[] cardsActiveness)
    {
        activatedCards = cardsActiveness;
    }

    public void SetCardsLocationsFromMemory()
    {
        if (cardPositions == null)
            Debug.Log(TAG + " cardPositions is null:(((");
        else
            Debug.Log(TAG + " cardPositions is OK");
        if (saveLoad == null)
            Debug.Log(TAG + " saveload is null:(((");
        else
            Debug.Log(TAG + " saveload is OK");

        // create the array of positions from memory
        for (int i = 0;i < 16; i++)
        {
            Debug.Log(TAG + " iteration "+i+" positino: " + saveLoad.GetInt("card" + i + "position"));

            cardPositions[i] = saveLoad.GetInt("card" + i+"position");
        }

        // set the transforms of the cards
        setAllCardsToTheirDecidedPositions();
    }
    // get the active status of each card and set the values to the array
    public void SetCardsActivenessFromMemory()
    {
        if (activatedCards == null)
            Debug.Log(TAG + " activatedCards is null:(((");
        else
            Debug.Log(TAG + " activatedCards is OK");

        for (int i = 0; i < 16; i++)
        {
            activatedCards[i] = saveLoad.GetBool("card" + i + "activeness");
        }
        SetAllCardsToTheirResolvedActiveness();
    }

    internal void RemoveCouple(int firstFlippedCardIndex, int secondFlippedCardIndex)
    {

        // flip back the cards (prepare them for the next game - it's the same GameObjects)
        allCards[firstFlippedCardIndex].transform.Rotate(new Vector3(0, 180, 0));
        allCards[secondFlippedCardIndex].transform.Rotate(new Vector3(0, 180, 0));
        // disappear the cards
        allCards[firstFlippedCardIndex].SetActive(false);
        allCards[secondFlippedCardIndex].SetActive(false);

        activatedCards[firstFlippedCardIndex] = false;
        activatedCards[secondFlippedCardIndex] = false;
    }

    // use the array of activated status to actually actived or deactived the cards
    public void SetAllCardsToTheirResolvedActiveness()
    {
        for (int i = 0; i < 16; i++)
        {
            allCards[i].SetActive(activatedCards[i]);
        }
    }

    public void SaveAllCardsLoctionsToMemory()
    {
        if (saveLoad == null)
            Debug.Log(TAG + " saveload is null");
        else
            Debug.Log(TAG + " saveload is OK");

        for (int i = 0; i < 16; i++)
        {
            Debug.Log(TAG + " save card " + i + "to position " + cardPositions[i]);
            saveLoad.SetInt("card" + i + "position", cardPositions[i]);
        }


    }

    public void SaveAllCardsActivenessToMemory()
    {
        if (activatedCards == null)
            Debug.Log(TAG + " activatedCards is null");
        else
            Debug.Log(TAG + " activatedCards is OK");

        for (int i = 0; i < 16; i++)
        {
            Debug.Log(TAG + " save card " + i + "to activneness " + activatedCards[i]);

            saveLoad.SetBool("card" + i + "activeness", activatedCards[i]);
        }
    }


}
