using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to move the camera for testing purposes.
/// </summary>
public class MoveCamera : MonoBehaviour {

	float rotation = 0;
	float position = 0;

	// Reacts to keyboard input and moves the camera around.
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
