using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadProvider
{
    public static readonly string SAVE_LOAD_OPT_KEY = "whichSaveLoaderToInstantiate";
    private readonly string TAG = "MoonActive";


    // implement singleton design pattern for the provider, as it is approached from different game objects
    // the consistency of the provider between game objects is a MUST,
    // otherwise save and load operation can accidentally in different storage methodologies
    private static SaveLoadProvider saveLoadProvider;
    private static SaveLoadPlayerPrefs saveLoadPlayerPrefs;
    private static SaveLoadLocal saveLoadLocal;

    private SaveLoadProvider() { }
    public static SaveLoadProvider getInstance()
    {
        if (saveLoadProvider != null)
            return saveLoadProvider;
        else
        {
            saveLoadProvider = new SaveLoadProvider();
            saveLoadPlayerPrefs = new SaveLoadPlayerPrefs();
            saveLoadLocal = new SaveLoadLocal();

            return saveLoadProvider;
        }
    }
    
    // this should read from the PlayerPref the correct type of concrete class and return it
    public ISaveLoad GetSaverLoader()
    {

        // STEP 1: access a file PlayerPref to decide which SaveLoad implementation to instantiate
        int opt = PlayerPrefs.GetInt(SAVE_LOAD_OPT_KEY, -1);
        Debug.Log(TAG + " SaveLoadProvider GetSaverLoader");
        Debug.Log(TAG + " opt is: "+ opt);
        switch (opt)
        {
            case 0:
                Debug.Log(TAG + " opt is: " + 0);
                return saveLoadPlayerPrefs;
                break;
            case 1:
                Debug.Log(TAG + " opt is: " + 1);

                return saveLoadLocal;
                break;
            /* this list can be extended for example case '2' can save things to text files...*/
            default:
                Debug.Log(TAG + " opt is: " + 0);

                return saveLoadPlayerPrefs;
        }
    }

    // create a text file that contain all the save\load options 
    public string[] GetAllSaveLoadOptions()
    {
        return new string[] { "Unity's PlayerPref", "local Variables" };
    }

    public void ChangeSaveLoadOption(int val) 
    {
        Debug.Log(TAG + " SaveLoadProvider ChangeSaveLoadOption");
        Debug.Log(TAG + " val is: " + val);

        PlayerPrefs.SetInt(SAVE_LOAD_OPT_KEY, val);
    }
}
