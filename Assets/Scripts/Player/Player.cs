using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;


public class Player : MonoBehaviour
{
    public Vector3 startPosition;
	private Quaternion startRotation;
    private PlayerData data;
    public GameObject playerObject;
    public int maxLives = 5;

    public float speed = 0.1f;
    public float jumpForce = 0.02f;
    float movement = 0.0f;
	bool isJumping = false;

    private float startTime;

	public float Movement {
		get{ 
			// Grab the current head transform and broadcast it to all the other users in the session
			Transform headTransform = Camera.main.transform;

			// Transform the head position and rotation into local space
			Vector3 headPosition = this.transform.InverseTransformPoint(headTransform.position);
			Quaternion headRotation = Quaternion.Inverse(this.transform.rotation) * headTransform.rotation;
			return speed * headRotation.z;
		}
	}

    public String PlayerName
    {
        get { return data.PlayerName; }
        set { data.PlayerName = value; }
    }

    public int Score
    {
		get { return data.Score; }
    }

	public float PlayerTimer
	{
		get { return Time.realtimeSinceStartup - startTime; }
	}

	public int WeightedScore
	{
		get { return data.WeightedScore; }
	}

	public int Deaths
	{
		get { return data.DeathCount; }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            collectItem(other.GetComponent<Item>());
        }
        else if (other.tag == "spikes")
        {
            die();
        }
        else if (other.tag == "portal")
        {
            win();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "border")
        {
            die();
        }
    }

    //Todo: Add respawn
    void die()
    {
        data.DeathCount++;
        if (data.DeathCount == maxLives)
        {
            NavigateToScene.GoToScene("Highscore");
        }
        else {

            //Reset Player Position and velocity
            if (gameObject.transform.position != startPosition)
            {
                ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
            }

            //playerObject.transform.position = startPosition;
            GameObject starter = null;
            GameObject[] all = FindObjectsOfType<GameObject>();

            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].tag == "startPosition")
                {
                    starter = all[i];
                }
            }
            if (starter != null)
            {
                playerObject.transform.position = starter.transform.position;
            }
            else
            {
                playerObject.transform.position = startPosition;
            }
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.playSoundAt(1, gameObject.transform);
            }

            
            playerObject.transform.rotation = startRotation;
            Rigidbody playerBody = playerObject.GetComponent<Rigidbody>();
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
        }
    }

    void win()
    {
		data.Score += (LevelManager.Instance.CurrentLevel+1) * 100;
        //navigates to the next level or the highscore view
        if (!LevelManager.Instance.LoadNextLevel())
        {
            NavigateToScene.GoToScene("Highscore");
        }
    }

    //Todo: Add special effects and sound
    void collectItem(Item item)
    {
        data.Score += item.ItemValue;
        ParticleSpawner.Instance.SpawnParticleSystem(1, item.gameObject.transform);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.playSound(0);
        }
        Destroy(item.gameObject);
    }

    public void run()
    {

        GameObject starter = null, reference = null;

        GameObject[] all = FindObjectsOfType<GameObject>();

        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].tag == "startPosition")
            {
                starter = all[i];
            }
            if (all[i].tag == "referencePosition")
            {
                reference = all[i];
            }
        }


        if (starter != null && reference != null)
        {
            float x = starter.transform.position.x - reference.transform.position.x;
            float z = starter.transform.position.z - reference.transform.position.z;
            /*
            if (Math.Abs(x) > Math.Abs(z))
            {
                //playerObject.transform.position = new Vector3(transform.position.x - (Movement * x), transform.position.y, transform.position.z - (Movement * z));
                if (x < 0)
                {
                    playerObject.transform.position = new Vector3(transform.position.x + Movement, transform.position.y, transform.position.z);
                }
                else
                {
                    playerObject.transform.position = new Vector3(transform.position.x - Movement, transform.position.y, transform.position.z);
                }

            }
            else
            {
                if (z < 0)
                {
                    playerObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Movement);
                }
                else
                {
                    playerObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Movement);
                }
                //playerObject.transform.position = new Vector3(transform.position.x - (Movement * x), transform.position.y, transform.position.z - (Movement * z));

            }
            */

            Vector3 move = playerObject.transform.position;
            //playerObject.transform.position = new Vector3(transform.position.x - (Movement * x), transform.position.y, transform.position.z - (Movement * z));
            if (x < 0)
            {
                move.x = move.x - Movement * x;
            }
            else
            {
                move.x = move.x + Movement * x;
            }

            if (z < 0)
            {
                move.z = move.z - Movement * z;
            }
            else
            {
                move.z = move.z + Movement * z;
            }
            //playerObject.transform.position = new Vector3(transform.position.x - (Movement * x), transform.position.y, transform.position.z - (Movement * z));

            playerObject.transform.position = move;

        }
        else
        {
            Debug.Log("SOMETHING IS NULL");
        }


        //playerObject.GetComponent<Rigidbody>().AddForce(new Vector3(movement, 0, 0));

    }

    public void jump()
    {
        Debug.Log("Jumping really high.......");
        if (!isJumping)
        {
            isJumping = true;
            playerObject.GetComponent<Rigidbody>().AddForce((-(Movement / speed * 15)), jumpForce, 0);
        }
    }

    void OnDestroy()
    {
        //Stores player data to the preferences
		data.Timer = PlayerTimer;
        PreferenceManager.WriteJsonToPreferences("player", data);
    }

    // Use this for initialization
    void Start()
    {
        //TODO: Get PlayerName
        data = PreferenceManager.ReadJsonFromPreferences<PlayerData>("player");
        if (data == null) {
        	data = new PlayerData();
        }
		startTime = Time.realtimeSinceStartup;
        startPosition = playerObject.transform.position;
		startRotation = playerObject.transform.rotation;
    }

	public Boolean debugMode;
	float testRotation = 0;
	float previousMovement = 0;

	void Update(){

		if (isJumping) {
            if (playerObject.GetComponent<Rigidbody>().velocity.y > 0) {
                playerObject.GetComponent<Rigidbody>().AddForce((-(Movement / speed * 25)), 0, 0);
            }

            else if (playerObject.GetComponent<Rigidbody>().velocity.y == 0) {
                isJumping = false;
                Rigidbody playerBody = playerObject.GetComponent<Rigidbody>();
                playerBody.velocity = Vector3.zero;
                playerBody.angularVelocity = Vector3.zero;
            }

		} else {
			run();
		}

		if (debugMode) {
			if (Input.GetKeyDown(KeyCode.W)) {
				jump();
			}
			if (Input.GetKey(KeyCode.A)) {
				testRotation++;
				Camera.main.transform.rotation = Quaternion.Euler(0, 0, testRotation);
			}
			if (Input.GetKey(KeyCode.S)) {
				testRotation = 0;
				Camera.main.transform.rotation = Quaternion.Euler (0, 0, testRotation);
			}
			if (Input.GetKey(KeyCode.D)) {
				testRotation--;
				Camera.main.transform.rotation = Quaternion.Euler (0, 0, testRotation);
			}
		}
	}

}

