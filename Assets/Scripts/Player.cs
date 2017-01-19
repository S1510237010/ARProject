using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData data;
    public GameObject playerObject;
    public HighscoreData Highscore;
    public float distance = 0.001f;

    public void ItemCollected(int points)
    {
        data.Score += points;
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
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (Highscore == null)
        {
            Highscore = new HighscoreData();
            Highscore.addItem(data);
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
