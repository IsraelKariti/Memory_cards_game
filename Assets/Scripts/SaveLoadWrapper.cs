using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is how i approach the Save and Load mechanism from the scene
public class SaveLoadWrapper : MonoBehaviour
{
    SaveLoadProvider mSaveLoadProvider;
    ISaveLoad mSaveLoad;
    private readonly string TAG = "MoonActive";

    private void Awake()
    {
        Debug.Log(TAG + " SaveLoadWrapper Awake NEW WRAPPER IS CREATED!!!");

        mSaveLoadProvider = SaveLoadProvider.getInstance();
        Debug.Log(TAG + "mSaveLoadProvider is null:  " + (mSaveLoadProvider == null));

        mSaveLoad = mSaveLoadProvider.GetSaverLoader();
        if(mSaveLoad == null)
            Debug.Log(TAG + "mSaveLoad is null");
        else
            Debug.Log(TAG + "mSaveLoad is OK");

        Debug.Log(TAG + "INSIDE NEW WRAPPER " + mSaveLoad.GetInt("card0position"));

    }



    public void ChangeSaveLoadOption(int val)
    {
        Debug.Log(TAG + " SaveLoadWrapper ChangeSaveLoadOption");
        Debug.Log(TAG + " val is: " + val);

        // change the configuration
        mSaveLoadProvider.ChangeSaveLoadOption(val);
        // get a new save load object
        mSaveLoad = mSaveLoadProvider.GetSaverLoader();
    }

    public ISaveLoad GetSaveLoad()
    {
        return mSaveLoad;
    }

    public string[] GetAllSaveLoadOptions()
    {
        return mSaveLoadProvider.GetAllSaveLoadOptions();
    }
}
