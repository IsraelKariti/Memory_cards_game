using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtnScript : MonoBehaviour
{
    public GameScript gameScript;
    public void setTheCards()
    {
        gameScript.StartTheGame();
    }
}
