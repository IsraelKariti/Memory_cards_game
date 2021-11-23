using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadPlayerPrefs : ISaveLoad
{
    public bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key)==1?true:false;
    }

    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SetBool(string key, bool val)
    {
        PlayerPrefs.SetInt(key, val?1:0);
    }

    public void SetFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }

    public void SetInt(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }

    public void SetString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }
}
