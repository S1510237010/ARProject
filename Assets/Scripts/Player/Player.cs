using System;
using UnityEngine;
using System.Collections;
using Academy.HoloToolkit.Unity;

/// <summary>
/// This class contains the player logic.
/// </summary>
public class Player : MonoBehaviour
{
	[Tooltip("The object that will be used as the player.")]
	public GameObject playerObject;
	[Tooltip("The number of lives a player has available for all levels.")]
	public int maxLives = 5;
	[Tooltip("Position of the players startpoint in the level.")]
	public GameObject startPosition;    
	[Tooltip("Reference Point to determine, how the level is placed in the room and how the player has to move (x and z Achse)")]
	public GameObject referencePosition;  
	[Tooltip("The distance the player moves in one itteration")]
	public float speed = 0.1f;
	[Tooltip("The force that is applied to a jump.")]
	public float jumpForce = 120f;
	[Tooltip("Enables the debug mode.")]
	public Boolean debugMode;

	private Quaternion startRotation;
    private PlayerData data;
	float testRotation = 0;
	bool isJumping = false;
    private float startTime;

	/// <summary>
	/// Gets the current movement.
	/// </summary>
	/// <value>The movement.</value>
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

	/// <summary>
	/// Gets the time that has elapsed since the level was started.
	/// </summary>
	/// <value>The player timer.</value>
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

	void OnEnable(){
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

	/// <summary>
	/// Initializes the new Player with already stored player data or creates a new data instance.
	/// It also stores the start rotation and the start time.
	/// </summary>
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

	/// <summary>
	/// Updates the player position/movement.
	/// </summary>
	void Update(){
		// Player movement
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

		// Handles the player movement if debug mode is activated
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

	/// <summary>
	/// This function stores the player data to the player preferences.
	/// </summary>
	void OnDestroy()
	{
		//Stores player data to the preferences
		data.Timer = PlayerTimer;
		PreferenceManager.WriteJsonToPreferences("player", data);
	}

	/// <summary>
	/// This function gets called when the player enters a trigger.
	/// It checks if the trigger that was entered has specific tags and calls the respective functions.
	/// </summary>
	/// <param name="other">The object the player collides with.</param>
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

	/// <summary>
	/// This function gets called when the player exits a trigger and calls a function if the trigger tag was "border".
	/// </summary>
	/// <param name="other">The object the player collides with.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "border")
        {
            die();
        }
    }

	/// <summary>
	/// This function gets called if the user collides with spikes or the level border and it resets the player to it's origin.
	/// </summary>
    void die()
    {
        data.DeathCount++;
        if (data.DeathCount == maxLives)
        {
            NavigateToScene.GoToScene("Highscore");
        }
        else {
			if(transform.position != startPosition.transform.position){
				// Instantiate explosion particle system
				if(ParticleSpawner.Instance != null)
					ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
			}
			// Play explosion sound effect
			if(SoundManager.Instance != null)
				SoundManager.Instance.playSoundAt(1, gameObject.transform);
            
			// Reset Player Position and velocity
            playerObject.transform.position = startPosition.transform.position;
            playerObject.transform.rotation = startRotation;
            Rigidbody playerBody = playerObject.GetComponent<Rigidbody>();
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
        }
    }

	/// <summary>
	/// This function is called when the portal is reached and loads the next level or the highscore scene.
	/// </summary>
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

	/// <summary>
	/// This function adds the points of an item to the player score and removes the respective item from the level.
	/// </summary>
	/// <param name="item">Item.</param>
    void collectItem(Item item)
    {
        data.Score += item.ItemValue;
		item.gameObject.SetActive (false);

		// Plays the item collection SFX
		if(ParticleSpawner.Instance != null)
        	ParticleSpawner.Instance.SpawnParticleSystem(1, item.gameObject.transform);
		if(SoundManager.Instance != null)
			SoundManager.Instance.playSoundAt(0, gameObject.transform);

		Destroy (item.gameObject);
    }

	/// <summary>
	/// This method makes the player move.
	/// It depends on how the position of the level is in the room and how the two empty gameobjects (startPosition and referencePosition) are
	/// positioned to each other. 
	/// </summary>
    public void run()
    {
        if (startPosition != null && referencePosition != null)
        {
            float x = startPosition.transform.position.x - referencePosition.transform.position.x;
            float z = startPosition.transform.position.z - referencePosition.transform.position.z;
            
			// Calculate the new position
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
		
	/// <summary>
	/// This method is called, when the player jumps. It is the movement of the Jump.
	/// </summary>
    public void jump()
    {
        if (!isJumping) // it is only possible to jump one time at a time
        {
			if (startPosition != null && referencePosition != null)
            {
                isJumping = true;
                // calculating the movement for the jump, so that the player jumps a bow
				float x = startPosition.transform.position.x - referencePosition.transform.position.x;
				float z = startPosition.transform.position.z - referencePosition.transform.position.z;
                playerObject.GetComponent<Rigidbody>().AddForce((-(Movement / speed * 15)) * x, jumpForce, (-(Movement / speed * 15)) * z);
            }
        }
    }

}

