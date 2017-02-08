using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;

/// <summary>
/// This class handles the creation/destruction of levels and puts them at it's own position/rotation
/// </summary>
public class LevelManager : MonoBehaviour {
	[Tooltip("Add all levels here in the order that they should be shown.")]
	public GameObject[] Levels;
	[Tooltip("Enables the debug mode.")]
	public bool debugMode;

	/// <summary>
	/// Stores the currently instantiated level.
	/// </summary>
    private GameObject levelObject;

	/// <summary>
	/// Stores the current level index
	/// </summary>
	private int currentLevel = 0;
	public int CurrentLevel{
		get{ return currentLevel; }
	}

	/// <summary>
	/// Singleton implementation
	/// </summary>
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

	// Displays the first level on startup & removes the stored player data of previous sessions
    void Start()
    {
		DisplayLevel();
		PlayerPrefs.DeleteKey ("player");
    }

	/// <summary>
	/// Checks if the level is placed and if it isn't already enabled it enables it and hides the mapping meshes
	/// </summary>
	void Update(){
		if (!levelObject.activeInHierarchy && !gameObject.GetComponent<Placeable>().IsPlacing) {
			FindObjectOfType<SpatialMappingManager> ().DrawVisualMeshes = false;
            levelObject.SetActive(true);
            //Debug.Log("SET ACTIVE");
			//Debug.Log(gameObject.transform.rotation);
			transform.rotation = Quaternion.Euler (0, 0, transform.rotation.z);
            //Debug.Log(gameObject.transform.rotation);
			if (debugMode) {
				StartCoroutine (Test());
			}
        }
    }

	/// <summary>
	/// Loops through all available levels
	/// </summary>
	public IEnumerator Test(){
		yield return new WaitForSeconds(5);
		if (LoadNextLevel ()) {
			StartCoroutine (Test());
		}
	}

	/// <summary>
	/// Instantiates the new level
	/// </summary>
	private void DisplayLevel(){
		//Debug.Log("Display Level " + CurrentLevel);
		levelObject = Instantiate<GameObject> (Levels [currentLevel]);
		levelObject.transform.SetParent(gameObject.transform, false);
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	/// <returns><c>true</c>, if next level was loaded, <c>false</c> otherwise.</returns>
	public bool LoadNextLevel(){
		currentLevel++;
		if (currentLevel < Levels.Length) {
			GameObject oldLevel = GameObject.FindGameObjectWithTag("level");
			if (oldLevel != null) {
				Destroy (oldLevel);
			}
			DisplayLevel();
			return true;
		} else {
			return false;
		}
	}


}
