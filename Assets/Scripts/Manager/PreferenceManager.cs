using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class offers static functions for data serialization & deserialization.
/// </summary>
public class PreferenceManager : MonoBehaviour
{
	/// <summary>
	/// Reads the json from preferences.
	/// </summary>
	/// <returns>The parsed object.</returns>
	/// <param name="key">The preference key.</param>
	/// <typeparam name="T">The type of the object that should be desiralized.</typeparam>
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

	/// <summary>
	/// Converts a given object to json and stores it in the player preferences.
	/// </summary>
	/// <param name="key">The preference key.</param>
	/// <param name="data">The data to store.</param>
	/// <typeparam name="T">The type of the object that should be serialized.</typeparam>
    public static void WriteJsonToPreferences<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
		PlayerPrefs.SetString(key, json);
		PlayerPrefs.Save();
    }
}
