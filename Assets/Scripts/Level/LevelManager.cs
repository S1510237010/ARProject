using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;
/**
 * This class handles the creation/destruction of levels and puts them at it's own position/rotation
 */
public class LevelManager : MonoBehaviour {
	public GameObject[] Levels;
	public bool debugMode;

    private GameObject levelObject;
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
        DisplayLevel();
    }

	void Update(){
		if (!levelObject.activeInHierarchy && !gameObject.GetComponent<Placeable>().IsPlacing) {
			FindObjectOfType<SpatialMappingManager> ().DrawVisualMeshes = false;
            levelObject.SetActive(true);
            Debug.Log("SET ACTIVE");
			transform.rotation = Quaternion.Euler (0, 0, transform.rotation.z);
            Debug.Log(gameObject.transform.rotation.y);
			if (debugMode) {
				StartCoroutine (Test());
			}
        }
		PlayerPrefs.DeleteKey ("player");
    }

	public IEnumerator Test(){
		yield return new WaitForSeconds(5);
		if (LoadNextLevel ()) {
			StartCoroutine (Test());
		}
	}

	private void DisplayLevel(){

		Debug.Log("Display Level " + CurrentLevel);

		levelObject = Instantiate<GameObject> (Levels [currentLevel]);
		levelObject.transform.SetParent(gameObject.transform, false);
        //levelObject.transform.position = new Vector3(-1f, -1f, -0.5f);
        //newLevel.transform.rotation.Set(0, 0, 0, 0);
	}

	//loads the next level prefab, returns false if there are no more levels
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
