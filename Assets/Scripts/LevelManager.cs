using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject[] Levels;
	public Vector3 levelPosition;
	private int currentLevel = 0;
	public int CurrentLevel{
		get{ return currentLevel; }
	}

	private static LevelManager instance;
	public static LevelManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<LevelManager>();
			}
			return instance;
		}
	}

	//loads the next level prefab, returns false if there are no more levels
	public bool LoadNextLevel(){
		if (currentLevel < Levels.Length) {
			Level oldLevel = FindObjectOfType<Level>();
			if (oldLevel != null) {
				Destroy (oldLevel.gameObject);
			}
			//TODO: place level correctly
			GameObject newLevel = Instantiate<GameObject> (Levels [currentLevel]);
			newLevel.transform.position = levelPosition;
			currentLevel++;
			return true;
		} else {
			return false;
		}
	}
}
