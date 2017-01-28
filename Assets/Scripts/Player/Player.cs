using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData data;
    //public HighscoreData Highscore;
    public GameObject playerObject;
    public float distance = 0.001f;

    public String PlayerName
    {
        get { return data.PlayerName; }
        set { data.PlayerName = value; }
    }

    public int Score
    {
        get { return data.Score; }
    }

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

    public void onRun()
    {
        Transform TrCamera = Camera.main.transform;

        if (TrCamera.rotation.z < 0)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.red;

            playerObject.transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }
        else if (TrCamera.rotation.z > 0)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.green;

            playerObject.transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
        }
        else
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void onJump()
    {
        //TODO: Play jump SFX
        //AudioSource.PlayClipAtPoint(SFK ,gameObject.transform.position);
    }
    /*
    public void onKill()
    {
        //TODO: Play death SFX
        //AudioSource.PlayClipAtPoint(SFK ,gameObject.transform.position);

        //Spawns explosion at player position
        //ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
        Destroy(gameObject);
    }
    
    void OnDestroy()
    {
        if (Highscore == null)
        {
            Highscore = new HighscoreData();
            Highscore.AddItem(data);
        }
    }
    */

    // Use this for initialization
    void Start () {
		//TODO: Get PlayerName
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
