using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceManager : MonoBehaviour
{
    public static T ReadJsonFromPreferences<T>(string key)
    {
        string json = PlayerPrefs.GetString(key);
        Debug.Log(key + " contains: " + json);
        return JsonUtility.FromJson<T>(json);
    }

    public static void WriteJsonToPreferences<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
        Debug.Log(key + " Json: " + json);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
}
