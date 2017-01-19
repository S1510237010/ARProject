using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighscoreData : MonoBehaviour
{
    private List<PlayerData> highscoreData;
    public GameObject highscoreItem;
    public int ListSize;
    public string PreferenceKey;

    // Use this for initialization
    void Start()
    {
        // highscoreData = PreferenceManager.ReadJsonFromPreferences<List<PlayerData>>(PreferenceKey);
        if (highscoreData == null || highscoreData.Count == 0)
        {
            highscoreData = new List<PlayerData>();
            for (int i = 0; i < 3; i++)
            {
                AddItem("Player " + (i + 1), UnityEngine.Random.Range(0, 9999));
            }
        }
        UpdateListView();
        Debug.Log("Finished Initialization");
    }

    void OnDestroy()
    {
        //PreferenceManager.WriteJsonToPreferences(PreferenceKey, highscoreData);
    }

    public void UpdateListView()
    {
        int i = 0;
        while (i < ListSize && i < highscoreData.Count)
        {
            GameObject menuItem = Instantiate(highscoreItem);
            menuItem.transform.parent = transform;
            
            Text[] contentText = menuItem.GetComponentsInChildren<Text>();
            foreach (var text in contentText)
            {
                if (text.name == "Score")
                {
                    text.text = highscoreData[i].Score.ToString();
                }
                if (text.name == "Player Name")
                {
                    text.text = highscoreData[i].PlayerName;
                }
            }
            Debug.Log("Initialized: " + highscoreData[i].PlayerName);

            i++;
        }
    }

    public void AddItem(PlayerData player)
    {
        bool inserted = false;
        for (int i = 0; i < highscoreData.Count; i++)
        {
            if (player.Score > highscoreData[i].Score)
            {
                highscoreData.Insert(i, player);
                inserted = true;
                i = highscoreData.Count;
                Debug.Log("Initialized["+i+" of "+highscoreData.Count+"]: " + player.Score);
            }
        }
        if (!inserted)
        {
            highscoreData.Add(player);
            Debug.Log("Initialized[" + (highscoreData.Count-1) + "]: " + player.Score);
        }
    }

    public void AddItem(String name, int score)
    {
        PlayerData p = new PlayerData();
        p.PlayerName = name;
        p.Score = score;

        AddItem(p);
    }
}
