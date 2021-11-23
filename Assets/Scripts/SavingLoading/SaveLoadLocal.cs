using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadLocal : ISaveLoad
{
    private Hashtable dictionary = new Hashtable();
    private readonly string TAG = "MoonActive";

    public Hashtable GetDictionary()
    {
        return dictionary;
    }
    public bool GetBool(string key)
    {
        return (bool)dictionary[key];
    }

    public float GetFloat(string key)
    {
        return (float)dictionary[key];
    }

    public int GetInt(string key)
    {
        Debug.Log(TAG + " SaveLoadLocal GetInt");
        if (dictionary == null)
            Debug.Log(TAG + " dictionary is null:(((");
        else
            Debug.Log(TAG + " dictionary is OK");
        Debug.Log(TAG + " dictionary count: "+ dictionary.Count);


        Debug.Log(TAG + " int is: "+(int)dictionary[key]);

        return (int)dictionary[key];
    }

    public string GetString(string key)
    {
        return (string)dictionary[key];
    }

    public void SetBool(string key, bool val)
    {
        dictionary[key]= val;
    }

    public void SetFloat(string key, float val)
    {
        dictionary.Add(key, val);
    }

    public void SetInt(string key, int val)
    {
        if (dictionary.ContainsKey(key))
            dictionary.Remove(key);
        dictionary.Add(key, val);
    }

    public void SetString(string key, string val)
    {
        dictionary.Add(key, val);
    }
}
