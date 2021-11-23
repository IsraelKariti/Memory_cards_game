using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class SaveLoadTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void simpleTestCase()
    {
        // Use the Assert class to test conditions
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();

        Assert.AreEqual(saveLoadProvider!=null, true);
    }
    
    

    [Test]
    public void GetSaveLoadInstance()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        saveLoadProvider.ChangeSaveLoadOption(0);

        ISaveLoad saveLoad = saveLoadProvider.GetSaverLoader();
        Assert.AreEqual(saveLoad is SaveLoadPlayerPrefs, true);
        Assert.AreEqual(saveLoad is SaveLoadLocal, false);
    }

    [Test]
    public void GetSaveLoadTypeStrings()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        string[] strs = saveLoadProvider.GetAllSaveLoadOptions();
        Assert.AreEqual(strs[0], "Unity's PlayerPref");
        Assert.AreEqual(strs[1], "local Variables");
    }

    

    [Test]
    public void ChangeTheSaveLoadType()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        saveLoadProvider.ChangeSaveLoadOption(1);
        ISaveLoad saveLoad = saveLoadProvider.GetSaverLoader();
        Assert.AreEqual(saveLoad is SaveLoadPlayerPrefs, false);
        Assert.AreEqual(saveLoad is SaveLoadLocal, true);
    }

    [Test]
    public void setTwice()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        saveLoadProvider.ChangeSaveLoadOption(0);

        ISaveLoad saveLoad = saveLoadProvider.GetSaverLoader();

        saveLoad.SetInt("firstInt", 1);
        int res = saveLoad.GetInt("firstInt");
        Assert.AreEqual(res, 1);

        saveLoad.SetInt("firstInt", 7);
        res = saveLoad.GetInt("firstInt");
        Assert.AreEqual(res, 7);

    }

    [Test]
    public void SaveLoadVarsInPlayerPrefs()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        saveLoadProvider.ChangeSaveLoadOption(0);

        ISaveLoad saveLoad = saveLoadProvider.GetSaverLoader();

        saveLoad.SetBool("firstBool", true);
        saveLoad.SetBool("secondBool", false);
        saveLoad.SetInt("firstInt", 22);
        saveLoad.SetInt("secondInt", -77799);
        saveLoad.SetFloat("firstFloat", 12.34f);
        saveLoad.SetFloat("secondFloat", -56.78f);
        saveLoad.SetString("firstString", "father");
        saveLoad.SetString("secondString", "mother");
        
        Assert.AreEqual(PlayerPrefs.GetInt("firstBool"), 1);
        Assert.AreEqual(PlayerPrefs.GetInt("secondBool"), 0);
        Assert.AreEqual(PlayerPrefs.GetInt("firstInt"), 22);
        Assert.AreEqual(PlayerPrefs.GetInt("secondInt"), -77799);
        Assert.AreEqual(PlayerPrefs.GetFloat("firstFloat"), 12.34f);
        Assert.AreEqual(PlayerPrefs.GetFloat("secondFloat"), -56.78f);
        Assert.AreEqual(PlayerPrefs.GetFloat("secondFloat")!= -56.78f, false);
        Assert.AreEqual(PlayerPrefs.GetString("firstString") , "father");
        Assert.AreEqual(PlayerPrefs.GetString("secondString") ,"mother");

        Assert.AreEqual(saveLoad.GetInt("firstBool"), 1);
        Assert.AreEqual(saveLoad.GetInt("secondBool"), 0);
        Assert.AreEqual(saveLoad.GetInt("firstInt"), 22);
        Assert.AreEqual(saveLoad.GetInt("secondInt"), -77799);
        Assert.AreEqual(saveLoad.GetFloat("firstFloat"), 12.34f);
        Assert.AreEqual(saveLoad.GetFloat("secondFloat"), -56.78f);
        Assert.AreEqual(saveLoad.GetFloat("secondFloat") != -56.78f, false);
        Assert.AreEqual(saveLoad.GetString("firstString"), "father");
        Assert.AreEqual(saveLoad.GetString("secondString"), "mother");
    }

    [Test]
    public void SaveLoadVarsLocallyReadFromAPI()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        saveLoadProvider.ChangeSaveLoadOption(1);
        SaveLoadLocal saveLoadLocal = (SaveLoadLocal)saveLoadProvider.GetSaverLoader();
        Assert.AreEqual(saveLoadLocal is SaveLoadLocal,true);
      


        saveLoadLocal.SetBool("firstBool", true);
        saveLoadLocal.SetBool("secondBool", false);
        saveLoadLocal.SetInt("firstInt", 22);
        saveLoadLocal.SetInt("secondInt", -77799);
        saveLoadLocal.SetFloat("firstFloat", 12.34f);
        saveLoadLocal.SetFloat("secondFloat", -56.78f);
        saveLoadLocal.SetString("firstString", "father");
        saveLoadLocal.SetString("secondString", "mother");

        

        Assert.AreEqual(saveLoadLocal.GetBool("firstBool"), true);
        Assert.AreEqual(saveLoadLocal.GetBool("secondBool"), false );
        Assert.AreEqual(saveLoadLocal.GetInt("firstInt"), 22);
        Assert.AreEqual(saveLoadLocal.GetInt("secondInt"), -77799);
        Assert.AreEqual(saveLoadLocal.GetFloat("firstFloat"), 12.34f);
        Assert.AreEqual(saveLoadLocal.GetFloat("secondFloat"), -56.78f);
        Assert.AreEqual(saveLoadLocal.GetFloat("secondFloat") != -56.78f, false);
        Assert.AreEqual(saveLoadLocal.GetString("firstString"), "father");
        Assert.AreEqual(saveLoadLocal.GetString("secondString"), "mother");
    }
    [Test]
    public void SaveLoadVarsLocallyReadFromHashtable()
    {
        SaveLoadProvider saveLoadProvider = SaveLoadProvider.getInstance();
        saveLoadProvider.ChangeSaveLoadOption(1);
        SaveLoadLocal saveLoadLocal = (SaveLoadLocal)saveLoadProvider.GetSaverLoader();
        Assert.AreEqual(saveLoadLocal is SaveLoadLocal,true);
        Hashtable hashtable = saveLoadLocal.GetDictionary();
        Assert.AreEqual(hashtable is Hashtable, true);


        saveLoadLocal.SetBool("firstBool", true);
        saveLoadLocal.SetBool("secondBool", false);
        saveLoadLocal.SetInt("firstInt", 22);
        saveLoadLocal.SetInt("secondInt", -77799);
        saveLoadLocal.SetFloat("firstFloat", 12.34f);
        saveLoadLocal.SetFloat("secondFloat", -56.78f);
        saveLoadLocal.SetString("firstString", "father");
        saveLoadLocal.SetString("secondString", "mother");

        Assert.AreEqual((bool)hashtable["firstBool"], true);
        Assert.AreEqual((bool)hashtable["secondBool"], false);
        Assert.AreEqual((int)hashtable["firstInt"], 22);
        Assert.AreEqual((int)hashtable["secondInt"], -77799);
        Assert.AreEqual((float)hashtable["firstFloat"], 12.34f);
        Assert.AreEqual((float)hashtable["secondFloat"], -56.78f);
        Assert.AreEqual((float)hashtable["secondFloat"] != -56.78f, false);
        Assert.AreEqual((string)hashtable["firstString"], "father");
        Assert.AreEqual((string)hashtable["secondString"], "mother");


    }
}
