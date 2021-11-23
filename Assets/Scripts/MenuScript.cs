using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject loadBtn;
    private void Start()
    {
 
        if (PlayerPrefs.GetInt("hasGameBeenSaved") == 1)
        {
            // there is a game to load, enable the load button
                loadBtn.SetActive(true);

        }
        else
        {
            loadBtn.SetActive(false);
        }
    }

    public void startGame()
    {
        PlayerPrefs.SetInt("gameShouldStartFromSaved", 0);

        SceneManager.LoadScene("GameScene");
    }

    public void loadGame()
    {
        
         // so that the game scene will know if the game is new or loaded
         PlayerPrefs.SetInt("gameShouldStartFromSaved", 1);

        SceneManager.LoadScene("GameScene");

    }
}
