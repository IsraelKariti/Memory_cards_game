using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoad
{
    public void SetInt(string key, int val);
    public void SetString(string key, string val);
    public void SetFloat(string key, float val);
    public void SetBool(string key, bool val);
    public int GetInt(string key);
    public string GetString(string key);
    public float GetFloat(string key);
    public bool GetBool(string key);
}
