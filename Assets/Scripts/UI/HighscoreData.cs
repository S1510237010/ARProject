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
        highscoreData = new List<PlayerData>();
        for (int i = 0; i < ListSize; i++)
        {
            PlayerData data = PreferenceManager.ReadJsonFromPreferences<PlayerData>(PreferenceKey + i);
            if (data != null)
            {
                highscoreData.Add(data);
            }
            else
            {
                i = ListSize;
            }
        }
        
        UpdateListView();
        Debug.Log("Finished Highscore Initialization");
    }

    void OnDestroy()
    {
       storeData();
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
                    text.text = highscoreData[i].Score.ToString() + " |";
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
            }
        }
        if (!inserted)
        {
            highscoreData.Add(player);
        }
    }

    public void AddItem(String name, int score)
    {
        PlayerData p = new PlayerData();
        p.PlayerName = name;
        p.Score = score;

        AddItem(p);
    }

    private void storeData()
    {
        int i = 0;
        while (i < highscoreData.Count && i < ListSize)
        {
            PreferenceManager.WriteJsonToPreferences<PlayerData>(PreferenceKey + i, highscoreData[i]);
            i++;
        }
    }
}
