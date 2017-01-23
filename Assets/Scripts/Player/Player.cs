using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData data;
    public GameObject playerObject;
    public float distance = 0.001f;
    public int PointsPerItem = 5;

    public String PlayerName
    {
        get { return data.PlayerName; }
        set { data.PlayerName = value; }
    }

    public int Score
    {
        get { return data.Score; }
    }

    public void ItemCollected()
    {
        data.Score += PointsPerItem;
    }

    /**
     *  Collission Event Handling for items, ... 
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            other.gameObject.SetActive(false);
            ItemCollected();
        }else if(other.gameObject.CompareTag("spike") || other.gameObject.CompareTag("border"))
        {
            onKill();
        }
    }

    public void onRun()
    {
        //TODO: Play jump SFX
        //AudioSource.PlayClipAtPoint(SFK ,gameObject.transform.position);


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

    public void onKill()
    {
        //TODO: Play death SFX
        //AudioSource.PlayClipAtPoint(SFK ,gameObject.transform.position);

        //Spawns explosion at player position
        ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
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


    // Use this for initialization
	void Start () {
		//TODO: Get PlayerName
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
