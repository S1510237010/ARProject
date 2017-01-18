using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreData : MonoBehaviour
{
    private static List<PlayerData> highscoreData;

    // Use this for initialization
    void Start()
    {
        highscoreData = new List<PlayerData>();
        int listSize = UnityEngine.Random.Range(3, 20);
        for (int i = 0; i < 10; i++)
        {
            addItem("Player " + (i + 1), UnityEngine.Random.Range(0, 9999));
        }
    }

    public void addItem(PlayerData player)
    {
        highscoreData.Add(player);
    }

    public void addItem(String name, int score)
    {
        PlayerData p = new PlayerData();
        p.PlayerName = name;
        p.Score = score;
        addItem(p);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
