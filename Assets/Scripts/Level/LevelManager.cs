using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class handles the creation/destruction of levels and puts them at it's own position/rotation
 */
public class LevelManager : MonoBehaviour {
	public GameObject[] Levels;
	private bool isInitialized = false;
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

    void Start()
    {
		PlayerPrefs.DeleteKey ("player");
    }

	void Update(){
		if (!GetComponent<Placeable> ().IsPlacing && !isInitialized) {
			DisplayLevel ();
			isInitialized = true;
		}
	}

	IEnumerator Test(){
		yield return new WaitForSeconds(5);
		LoadNextLevel ();
	}

	private void DisplayLevel(){
		GameObject newLevel = Instantiate<GameObject> (Levels [currentLevel]);

		newLevel.transform.SetParent(gameObject.transform, true);
        //newLevel.transform.rotation.Set(0, 0, 0, 0);
		//Set the new Player for the sound manager
		Player player = gameObject.GetComponentInChildren<Player>();
        player.enabled = false;
		if (player != null) {
			SoundManager.Instance.Player = player.gameObject;
			Debug.Log (SoundManager.Instance.Player.name);
		}
	}

	//loads the next level prefab, returns false if there are no more levels
	public bool LoadNextLevel(){
		currentLevel++;
		if (currentLevel < Levels.Length) {
			Level oldLevel = FindObjectOfType<Level>();
			if (oldLevel != null) {
				Destroy (oldLevel.gameObject);
			}
			DisplayLevel();
			return true;
		} else {
			return false;
		}
	}


}
