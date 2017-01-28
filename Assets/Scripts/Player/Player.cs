using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData data;
    public HighscoreData Highscore;
    public float speed = 0.001f;

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
            gameObject.GetComponent<Renderer>().material.color = Color.red;

            gameObject.transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
        else if (TrCamera.rotation.z > 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;

            gameObject.transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
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
            Highscore.AddItem(data);
        }
    }


    // Use this for initialization
	void Start () {
		//TODO: Get PlayerName
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        onRun();
	}
}
