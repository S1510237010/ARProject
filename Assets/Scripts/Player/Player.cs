using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData data;
    public GameObject playerObject;
    public HighscoreData Highscore;
    

    public void ItemCollected(int points)
    {
        data.Score += points;
    }

    public void onRun()
    {
        //TODO: Play jump SFX
        //AudioSource.PlayClipAtPoint(SFK ,gameObject.transform.position);
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
	void Update () {
		
	}
}
