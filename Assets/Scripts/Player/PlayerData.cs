using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {
    public string PlayerName;
	public int DeathCount;
    public int Score;
	public float Timer;
	public int WeightedScore{
		get{ 
			return (int) (Score / ((DeathCount+1.0)/2.0));
		}
	}
	public PlayerData(string name){
		PlayerName = name;
		Score = 0;
		DeathCount = 0;
		Timer = 0;
	}
}

