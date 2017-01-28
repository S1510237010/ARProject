using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Creates Visualization of Game borders
 */
public class Border : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MeshFilter meshFilter = GetComponent();

        if (meshFilter == null) {
            Debug.LogError("MeshFilter not found!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
