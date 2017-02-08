using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	/// <summary>
	/// Prevents the destruction of a GameObject when the scene is changed.
	/// </summary>
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
     
}
