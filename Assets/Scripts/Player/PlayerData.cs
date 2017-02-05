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
	private DateTime date;

	public int WeightedScore{
		get{ 
			return (int) (Score / ((DeathCount+1.0)/2.0));
		}
	}

	public String Date{
		get { return String.Format ("{0:dd.MM.yyyy}", date); }
	}

	public PlayerData(){
		date = DateTime.Now;
		PlayerName = "";
		Score = 0;
		DeathCount = 0;
		Timer = 0;
	}
}

