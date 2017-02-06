using UnityEngine;
using Academy.HoloToolkit.Unity;
public class Level : MonoBehaviour {
	public string name;

	void OnStart(){
		Rigidbody body = FindObjectOfType<Player> ().GetComponent<Rigidbody> ();
		//body.useGravity = false;
		body.isKinematic = true;
	}

	void OnEnable(){
		//FindObjectOfType<PlaySpaceManager> ().gameObject.SetActive (false);
	}

	void OnDisable(){
		//FindObjectOfType<PlaySpaceManager> ().gameObject.SetActive (true);
	}
}


