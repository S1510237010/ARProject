using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class handles the creation/destruction of levels and puts them at it's own position/rotation
 */
public class LevelManager : MonoBehaviour {
	public GameObject[] Levels;
	//public Vector3 levelPosition;
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
			//newLevel.transform.position = levelPosition;
			newLevel.transform.position = gameObject.transform.position;
			newLevel.transform.rotation = gameObject.transform.rotation;
			currentLevel++;
			return true;
		} else {
			return false;
		}
	}
}
