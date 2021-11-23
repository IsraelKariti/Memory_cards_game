using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this class exist to avoid coupling
public class DropDownFunctions : MonoBehaviour
{
    public SaveLoadWrapper saveLoadWrapper;
    public GameObject dropdownGO;

    private readonly string TAG = "MoonActive";
    private static readonly string SAVE_LOAD_OPT_KEY = "whichSaveLoaderToInstantiate";

    // Start is called before the first frame update
    // this is just a general class that inializes the game
    void Start()
    {

        // take of the drop down save load option
        string[] saveLoadOptions = saveLoadWrapper.GetAllSaveLoadOptions();

        // get all the text for the drop down options of save and load
        TMP_Dropdown dropdown = dropdownGO.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();
        foreach (string str in saveLoadOptions)
            dropdown.options.Add(new TMP_Dropdown.OptionData(str));

        switch (PlayerPrefs.GetInt(SAVE_LOAD_OPT_KEY))
        {
            case 0:
                dropdown.value = 0;
                break;
            case 1:
                dropdown.value = 1;
                break;
            default:
                dropdown.value = 0;
                break;

        }


    }
    public void changeSaveLoadDataMechanism(int val)
    {
        Debug.Log(TAG + " DropDownFunctions changeSaveLoadDataMechanism");
        Debug.Log(TAG + " val is: " + val);

        saveLoadWrapper.ChangeSaveLoadOption(val);
    }
}
