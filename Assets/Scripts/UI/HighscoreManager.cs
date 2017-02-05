using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighscoreManager: MonoBehaviour
{
    private List<PlayerData> highscoreData;
    public GameObject highscoreItem;
    public int ListSize;
    public string PreferenceKey;

    // Use this for initialization
    void Start()
    {
        //LoadData();
		LoadMockupData();
        PlayerData currentPlayerData = PreferenceManager.ReadJsonFromPreferences<PlayerData>("player");
        AddItem(currentPlayerData);
        if (highscoreItem != null)
        {
            UpdateListView();
        }
        Debug.Log("Finished Highscore Initialization");
    }

    void OnDestroy()
    {
       storeData();
    }

	private void LoadMockupData(){
		highscoreData = new List<PlayerData> ();
		System.Random rand = new System.Random ();
		for (int i = 0; i < 5; i++) {
			PlayerData data = new PlayerData();
			data.Score = rand.Next (1, 100) * 10;
			data.DeathCount = rand.Next (0, 10);
			AddItem (data);
		}
	}

    public void LoadData()
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
    }

    public void UpdateListView()
    {
        int i = 0;
        while (i < ListSize && i < highscoreData.Count)
        {
            GameObject menuItem = Instantiate(highscoreItem);
			menuItem.transform.SetParent(transform, false);
            
            Text[] contentText = menuItem.GetComponentsInChildren<Text>();
            foreach (var text in contentText)
            {
                if (text.name == "Score")
                {
					text.text = highscoreData[i].WeightedScore.ToString();
                }
                if (text.name == "Player Name")
                {
                    text.text = highscoreData[i].PlayerName;
                }
				if (text.name == "Date") {
					text.text = highscoreData[i].Date;
				}
            }
            Debug.Log("Initialized: " + highscoreData[i].PlayerName);

            i++;
        }
    }

    public void AddItem(PlayerData player)
    {
        bool inserted = false;
        if (player != null)
        {
            for (int i = 0; i < highscoreData.Count; i++)
            {
				if (player.WeightedScore > highscoreData[i].WeightedScore)
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
