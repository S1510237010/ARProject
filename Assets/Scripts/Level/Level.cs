using UnityEngine;
using Academy.HoloToolkit.Unity;
public class Level : MonoBehaviour {
	public string name;

	void OnEnable(){
		//FindObjectOfType<PlaySpaceManager> ().gameObject.SetActive (false);
	}

	void OnDisable(){
		//FindObjectOfType<PlaySpaceManager> ().gameObject.SetActive (true);
	}
}


