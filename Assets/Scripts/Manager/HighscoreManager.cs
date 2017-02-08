using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the highscore entries.
/// </summary>
public class HighscoreManager: MonoBehaviour
{
	/// <summary>
	/// The highscore item prefab that will be used for the list.
	/// </summary>
    public GameObject highscoreItem;

	/// <summary>
	/// The number of entries in the highscore list
	/// </summary>
    public int ListSize;

	/// <summary>
	/// The preference key that is used for highscore data storage.
	/// </summary>
	public string PreferenceKey;
	private List<PlayerData> highscoreData;

	/// <summary>
	/// Initalizes the highscore list.
	/// </summary>
    void Start()
    {
        LoadData();
		//LoadMockupData();
        PlayerData currentPlayerData = PreferenceManager.ReadJsonFromPreferences<PlayerData>("player");
        AddItem(currentPlayerData);
        if (highscoreItem != null)
        {
            UpdateListView();
        }
        //Debug.Log("Finished Highscore Initialization");
    }

	/// <summary>
	/// Serializes the updated highscore list when it is destroyed.
	/// </summary>
    void OnDestroy()
    {
       storeData();
    }

	/// <summary>
	/// Loads the data from the player preferences.
	/// </summary>
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

	/// <summary>
	/// Updates the highscore list view.
	/// </summary>
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
            //Debug.Log("Initialized: " + highscoreData[i].PlayerName);

            i++;
        }
    }

	/// <summary>
	/// Adds the given item at the corrected position in the highscore list.
	/// </summary>
	/// <param name="player">The player data to be added.</param>
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

	/// <summary>
	/// Stores the data to the player prefabs.
	/// </summary>
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
