using System;
using UnityEngine;
using System.Collections;
using Academy.HoloToolkit.Unity;


public class Player : MonoBehaviour
{
	public GameObject startPosition;
	public GameObject referencePosition;
	private Quaternion startRotation;
    private PlayerData data;
    public GameObject playerObject;
    public int maxLives = 5;

    public float speed = 0.1f;
    public float jumpForce = 0.02f;
	public Boolean debugMode;
	float testRotation = 0;
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


	// Use this for initialization
	void Start()
	{
		data = PreferenceManager.ReadJsonFromPreferences<PlayerData>("player");
		if (data == null) {
			data = new PlayerData();
		}
		if (debugMode) {
			data = new PlayerData ();
		}
		startTime = Time.realtimeSinceStartup;
		startRotation = playerObject.transform.rotation;
	}

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

	void OnDestroy()
	{
		//Stores player data to the preferences
		data.Timer = PlayerTimer;
		PreferenceManager.WriteJsonToPreferences("player", data);
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

    void die()
    {
        data.DeathCount++;
        if (data.DeathCount == maxLives)
        {
            NavigateToScene.GoToScene("Highscore");
        }
        else {
            //Reset Player Position and velocity
			if(transform.position != startPosition.transform.position){
				ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
			}
			SoundManager.Instance.playSoundAt(1, gameObject.transform);
            
            playerObject.transform.position = startPosition.transform.position;
            
            
            playerObject.transform.rotation = startRotation;
            Rigidbody playerBody = playerObject.GetComponent<Rigidbody>();
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
        }
    }

    void win()
    {
		if (LevelManager.Instance != null) {
			data.Score += (LevelManager.Instance.CurrentLevel + 1) * 100;
			//navigates to the next level or the highscore view
			if (!LevelManager.Instance.LoadNextLevel ()) {
				NavigateToScene.GoToScene ("Highscore");
			}
		} else {
			NavigateToScene.GoToScene ("Highscore");
		}
    }

    void collectItem(Item item)
    {
        data.Score += item.ItemValue;
		item.gameObject.SetActive (false);
        ParticleSpawner.Instance.SpawnParticleSystem(1, item.gameObject.transform);
		SoundManager.Instance.playSoundAt(0, gameObject.transform);
        
		Destroy (item.gameObject);
    }

    public void run()
    {
        if (startPosition != null && referencePosition != null)
        {
            float x = startPosition.transform.position.x - referencePosition.transform.position.x;
            float z = startPosition.transform.position.z - referencePosition.transform.position.z;
            
			//Calculate the new position
            Vector3 move = playerObject.transform.position;
            if (x < 0)
            {
                move.x += Movement * x;
            }
            else
            {
                move.x -= Movement * x;
            }

            if (z < 0)
            {
                move.z += Movement * z;
            }
            else
            {
                move.z -= Movement * z;
            }

            playerObject.transform.position = move;

        }
        else
        {
            Debug.Log("Run: Missing positioning information!");
        }

    }

    public void jump()
    {
        if (!isJumping)
        {
			if (startPosition != null && referencePosition != null)
            {
                isJumping = true;
				float x = startPosition.transform.position.x - referencePosition.transform.position.x;
				float z = startPosition.transform.position.z - referencePosition.transform.position.z;
                playerObject.GetComponent<Rigidbody>().AddForce((-(Movement / speed * 15)) * x, jumpForce, (-(Movement / speed * 15)) * z);
            }
        }
    }

}

