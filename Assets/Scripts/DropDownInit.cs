using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownInit : MonoBehaviour
{
    public SaveLoadWrapper saveLoadWrapper;
    public GameObject dropdownGO;
    // Start is called before the first frame update
    // this is just a general class that inializes the game
    void Start()
    {

        // take of the drop down save load option
        string[] saveLoadOptions = saveLoadWrapper.GetAllSaveLoadOptions();

        // get all the text for the drop down options of save and load
        TMP_Dropdown dropdown = dropdownGO.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();
        foreach(string str in saveLoadOptions)  
            dropdown.options.Add(new TMP_Dropdown.OptionData(str));

    }

    
}
