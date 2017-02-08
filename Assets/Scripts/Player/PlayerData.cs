using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	This class is used to persist the player data.  
 */
[Serializable]
public class PlayerData {
    public string PlayerName;
	public int DeathCount;
    public int Score;
	public float Timer;
	private DateTime date;

	/**
	 * A property that returns the final weighted score based on the collected points and deaths
	 */
	public int WeightedScore{
		get{ 
			return (int) (Score / ((DeathCount+1.0)/2.0));
		}
	}

	/**
	 * This returns the formated date when the player was created.
	 */
	public String Date{
		get { return String.Format ("{0:dd.MM.yyyy}", date); }
	}

	/*
	 * Empty Constructor to initialize all fields correctly
	 */
	public PlayerData(){
		date = DateTime.Now;
		PlayerName = "";
		Score = 0;
		DeathCount = 0;
		Timer = 0;
	}
}

