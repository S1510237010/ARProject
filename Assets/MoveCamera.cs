using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	float rotation = 0;
	float position = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rotation++;
			transform.rotation = Quaternion.Euler(0, rotation, 0);
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			position += 0.1f;
			transform.position = new Vector3 (0, position, 0);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			position -= 0.1f;
			transform.position = new Vector3 (0, position, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			rotation--;
			Camera.main.transform.rotation = Quaternion.Euler (0, rotation, 0);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Placeable place = FindObjectOfType<Placeable> ();
			if (place != null) {
				place.OnSelect ();
			}
		}
	}
}
