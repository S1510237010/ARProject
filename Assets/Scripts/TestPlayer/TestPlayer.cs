using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
