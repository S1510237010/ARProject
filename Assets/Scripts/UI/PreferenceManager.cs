using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceManager : MonoBehaviour
{

	public static void Write(String key, String value){
		PlayerPrefs.SetString(key, value);
		PlayerPrefs.Save();
	}

    public static T ReadJsonFromPreferences<T>(string key)
    {
        string json = PlayerPrefs.GetString(key);
        Debug.Log(key + " contains: " + json);
        T data = default(T);
        try
        {
            data = JsonUtility.FromJson<T>(json);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return data;
        
    }

    public static void WriteJsonToPreferences<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
		Write (key, json);
    }
}
