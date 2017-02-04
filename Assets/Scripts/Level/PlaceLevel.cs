using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceLevel : MonoBehaviour {

    bool placed = false;
    public GameObject level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (placed)
        {
            level.GetComponent<SimpleTagalong>().enabled = false;
        }
        else
        {
            
        }
    }

}
