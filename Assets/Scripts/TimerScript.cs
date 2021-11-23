using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timer;
    public bool playerWantsTokeepGoing;
    public GameScript gameScript;
    public SaveLoadWrapper saveLoadWrapper;

    private ISaveLoad saveLoad;
    private int totalTime;
    private IEnumerator co;
    private readonly string TAG = "MoonActive";

    private void Start()
    {
        saveLoad = saveLoadWrapper.GetSaveLoad();
    }

    public void Start30SecTimer()
    {
        Debug.Log(TAG + " startingg the timer");

        timer.gameObject.SetActive(true);
        totalTime = 30;
        timer.text = "" + totalTime;
        playerWantsTokeepGoing = true;
        co = DecreaseSeconds();
        StartCoroutine(co);
    }    
    public void stopTimer()
    {
        if(co!=null)
            StopCoroutine(co);
    }
    private IEnumerator DecreaseSeconds()
    {
        while (totalTime>0 && playerWantsTokeepGoing)
        {
            yield return new WaitForSeconds(1);
            totalTime--;
            timer.text = "" + totalTime;
        }
        if(playerWantsTokeepGoing)
            gameScript.timeEnded();
    }

    public void StartTimerFromMemory()
    {
        totalTime = saveLoad.GetInt("timer");
        timer.gameObject.SetActive(true);
        timer.text = "" + totalTime;
        playerWantsTokeepGoing = true;
        co = DecreaseSeconds();
        StartCoroutine(co);
    }

    public int GetTotalSec()
    {
        return totalTime;
    }

    public void SaveTimerToMemory()
    {
        saveLoad.SetInt("timer", totalTime);
    }
}
