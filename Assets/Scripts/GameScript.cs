using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScript : MonoBehaviour
{
    public GameObject deck;
    public AllCardsScript allCardsScript;
    public TimerScript timerScript;
    public Text winlose;
    public GameObject saveBtn;
    public SaveLoadWrapper saveLoadWrapper;
    public GameObject nextLevelBtn;
    public TMP_Text levelText;


    private readonly string TAG = "MoonActive";
    private ISaveLoad saveLoad;
    private List<GameObject> cards;
    private bool gameOn;
    private bool gameInteractive;// if animation is happening the game stops being interactive
    private int numberOfFlippedCards;
    private int firstFlippedCardIndex;
    private int secondFlippedCardIndex;
    private GameObject firstFlippedCardObject;
    private GameObject secondFlippedCardObject;
    private int numberOfUndiscoveredCouples;
    private int levelNumber;

    private void Start()
    {
        Debug.Log(TAG + " GameScript Start");
        saveLoad = saveLoadWrapper.GetSaveLoad();
        // create an array of all the 16 cards
        cards = new List<GameObject>();
        gameOn = false;
        gameInteractive = false;
        foreach (Transform child in deck.transform)
        {
            cards.Add(child.gameObject);
        }
        StartTheGame();
    }

    

    public void StartTheGame()
    {
        // general game settings
        winlose.gameObject.SetActive(false);
        gameOn = true;
        gameInteractive = true;

        Debug.Log(TAG + " GameScript StartTheGame");
        Debug.Log(TAG + " choose if scratch or saved");
        // check if game should be started new or load a saved one
        if (PlayerPrefs.GetInt("gameShouldStartFromSaved") == 0)
        {
            StartGameFromScratch();
        }
        else
        {

            Debug.Log(TAG + " decision: start from SAVED");
            // reset
            PlayerPrefs.SetInt("gameShouldStartFromSaved", 0);
            // start
            StartGameFromSaved();
        }
    }

    public void StartGameFromSaved()
    {
        Debug.Log(TAG + " GameScript StartGameFromSaved");
        Debug.Log(TAG + " call SetCardsLocationsFromMemory");

        allCardsScript.SetCardsLocationsFromMemory();
        Debug.Log(TAG + " call SetCardsActivenessFromMemory");

        allCardsScript.SetCardsActivenessFromMemory();

        Debug.Log(TAG + " call LoadLevelNumber");

        LoadLevelNumber();
        numberOfUndiscoveredCouples = saveLoad.GetInt("undiscoveredCouples");
        Debug.Log(TAG + " call stopTimer");

        timerScript.stopTimer();
        timerScript.StartTimerFromMemory();

        
        if (numberOfUndiscoveredCouples == 0)

        {
            wonGame();
        }



        
    }

    public void StartGameFromScratch()
    {
        levelNumber = 0;
        StartNewLevel();
        
    }

    public void StartNewLevel()
    {
        Debug.Log(TAG + "StartNewLevel");

        Debug.Log(TAG + "nextLevelBtn.SetActive");

        nextLevelBtn.SetActive(false);

        Debug.Log(TAG + "winlose.gameObject.SetActive");

        winlose.gameObject.SetActive(false);
        levelNumber++;
        Debug.Log(TAG + "levelText.text");

        levelText.text = "" + levelNumber;

        Debug.Log(TAG + "allCardsScript.setAllCardsRandomly");
        allCardsScript.setAllCardsRandomly();
        Debug.Log(TAG + "allCardsScript.setAllCardsToAcitve");

        allCardsScript.setAllCardsToAcitve();

        timerScript.stopTimer();

        timerScript.Start30SecTimer();

        numberOfUndiscoveredCouples = 8;
    }

    void Update()
    {
        if (gameOn && gameInteractive && numberOfUndiscoveredCouples>0)
        {
            StartCoroutine(gameStateMachine());
        }
    }

    IEnumerator gameStateMachine()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameObject touchedCard = getTheTouchedCard();
            if (touchedCard != null)
            {
                if (numberOfFlippedCards == 0)
                {
                    numberOfFlippedCards = 1;
                    firstFlippedCardIndex = cards.IndexOf(touchedCard);
                    //touchedCard.transform.rotation = Quaternion.Lerp(touchedCard.transform.rotation, Quaternion.Euler( touchedCard.transform.rotation.z, touchedCard.transform.rotation.x, touchedCard.transform.rotation.y + 180f), Time.deltaTime * 0.5f);
                    touchedCard.transform.Rotate(new Vector3(0, 180, 0));
                    firstFlippedCardObject = touchedCard;
                }
                else if (numberOfFlippedCards == 1 && (secondFlippedCardIndex = cards.IndexOf(touchedCard)) != firstFlippedCardIndex)
                {
                    numberOfFlippedCards = 2;
                    touchedCard.transform.Rotate(new Vector3(0, 180, 0));
                    secondFlippedCardObject = touchedCard;
                    bool isSimilar = checkIfCardsSimilar();
                    gameInteractive = false;
                    yield return new WaitForSeconds(0.6f);
                    gameInteractive = true;

                    // if the cards flipped are identical
                    if (isSimilar)
                    {
                        Debug.Log("similarr yes");
                        // reset the number of flipped cards
                        numberOfFlippedCards = 0;
                        allCardsScript.RemoveCouple(firstFlippedCardIndex, secondFlippedCardIndex);    
                        firstFlippedCardIndex = -1;
                        secondFlippedCardIndex = -1;
                        
                        firstFlippedCardObject = null;
                        secondFlippedCardObject = null;

                        // update the number of card couples still in the game
                        numberOfUndiscoveredCouples--;

                        if(numberOfUndiscoveredCouples == 0)
                        {
                            wonGame();
                        }

                    }
                    else
                    {
                        Debug.Log("similarr NOT");
                        numberOfFlippedCards = 0;
                        firstFlippedCardIndex = -1;
                        secondFlippedCardIndex = -1;
                        firstFlippedCardObject.transform.Rotate(new Vector3(0, 180, 0));
                        secondFlippedCardObject.transform.Rotate(new Vector3(0, 180, 0));
                    }

                }
            }
        }
    }

    

    private bool checkIfCardsSimilar()
    {
        if (    firstFlippedCardIndex % 2 == 0 && secondFlippedCardIndex == firstFlippedCardIndex + 1 ||
                secondFlippedCardIndex % 2 == 0 && firstFlippedCardIndex == secondFlippedCardIndex + 1)
            return true;
        else
            return false;
    }

    private GameObject getTheTouchedCard()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                GameObject touchedObject = hit.transform.parent.gameObject;

                Debug.Log("Touched " + touchedObject.transform.name + " contained: " + cards.Contains(touchedObject) + "at index: " + cards.IndexOf(touchedObject));

                return touchedObject;
            }
            else
                return null;
        }
        return null;
    }
    private void wonGame()
    {
        // stop the clock
        timerScript.playerWantsTokeepGoing = false;
        winlose.gameObject.SetActive(true);
        winlose.text = "YOU WON!";
        timerScript.stopTimer();
        nextLevelBtn.SetActive(true);
    }
    public void timeEnded()
    {
        if (gameOn)
        {
            gameOn = false;

            // set loser text 
            winlose.gameObject.SetActive(true);
            winlose.text = "YOU LOST";
            saveBtn.SetActive(false);


            StartCoroutine(returnToMenuSceneAfter5Sec());

        }
    }

    

    public int GetNumberOfUndiscoveredCouples()
    {
        return numberOfUndiscoveredCouples;
    }
    public void SetNumberOfUndiscoveredCouples(int num)
    {
        numberOfUndiscoveredCouples = num;
    }

    public void FinishTheSavedGame()
    {
        Debug.Log(TAG + " GameScript FinishTheSavedGame");

        gameOn = false;
        timerScript.playerWantsTokeepGoing = false;
        winlose.gameObject.SetActive(true);
        winlose.text = "GAME SAVED...";
        timerScript.stopTimer();
        StartCoroutine(returnToMenuSceneAfter5Sec());

    }

    private IEnumerator returnToMenuSceneAfter5Sec()
    {
        Debug.Log(TAG+"before we go back..returnToMenuSceneAfter5Sec...");

        yield return new WaitForSeconds(5);
        Debug.Log(TAG + "before we go back.....");
        Debug.Log(TAG + "before we go back..... card 0: " + saveLoad.GetInt("card0position"));
        SceneManager.LoadScene("StartMenu");

    }
    public void SaveLevelNumber()
    {
        saveLoad.SetInt("levelNumber", levelNumber);
    }
    public void LoadLevelNumber()
    {
        levelNumber = saveLoad.GetInt("levelNumber");
        levelText.text = "" + levelNumber;
        Debug.Log(TAG + "level number issssssssss: " + levelNumber);
    }
}
