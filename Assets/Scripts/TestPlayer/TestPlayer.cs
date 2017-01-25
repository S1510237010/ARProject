using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            //Add points to highscore
            collectItem(other.GetComponent<Item>());
        }

        if (other.tag == "spikes")
        {
            //Kill the player
            die();
            //Navigate to the next scene
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "border")
        {
            die();
        }
    }

    //Todo: Add respawn and sound
    void die()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
        int duration = ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
    }

    //Todo: Add special effects and sound
    void collectItem(Item item)
    {
        Debug.Log("+" + item.ItemValue + " Points!");
        ParticleSpawner.Instance.SpawnParticleSystem(1, item.gameObject.transform);
        Destroy(item.gameObject);
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
