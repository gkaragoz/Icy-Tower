using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem 
{
    public enum Language
    {
        English,
        Turkish,
        French
    }

    public static Language language = Language.French;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedFR;
    private static Dictionary<string, string> localisedTR;

    public static bool isInit;


    public static CSVLoader csvLoader;


    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();
        UpdateDictionaries();
        isInit = true;
    }

    public static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedFR = csvLoader.GetDictionaryValues("fr");
        localisedTR = csvLoader.GetDictionaryValues("tr");
    }
    
    public static Dictionary<string,string> GetDictionaryForEditor()
    {
        if (!isInit)
        {
            Init();
        }
        return localisedEN;
    }


    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;
        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key,out value);                
                break;
            case Language.French:
                localisedFR.TryGetValue(key, out value);
                break;
            case Language.Turkish:
                localisedTR.TryGetValue(key,out value);
                break;
        }
        return value;
        
    }


    public static void Add(string key,string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"','\"');
        }

        if (csvLoader==null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key,value);
        csvLoader.LoadCSV();

        UpdateDictionaries();

    }

    public static void Replace(string key,string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"','\"');
        }

        if (csvLoader==null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();

    }

    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }


}
