using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;


public class Player : MonoBehaviour
    {
		private Vector3 startPosition;
        private PlayerData data;
        public GameObject playerObject;
        public float speed = 0.1f;
        float movement = 0.0f;
        private float startTime;
        bool alive;

        public String PlayerName
        {
            get { return data.PlayerName; }
            set { data.PlayerName = value; }
        }

        public int Score
        {
			get { return data.WeightedScore; }
        }

        void OnTriggerEnter(Collider other)
        {
			if (other.tag == "item") {
				collectItem (other.GetComponent<Item> ());
			} else if (other.tag == "spikes") {
				die ();
			} else if (other.tag == "portal") {
				win ();
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
            alive = false;
			data.DeathCount++;
			//Debug.Log("Death count: " + data.DeathCount);

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.playSoundAt(1, gameObject.transform);
            }

			//Reset Player Position and velocity
            int duration = ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
			playerObject.transform.position = startPosition;
			Rigidbody playerBody = playerObject.GetComponent<Rigidbody> ();
			playerBody.velocity = Vector3.zero;
			playerBody.angularVelocity = Vector3.zero;
            
        }

		void win() {
			//navigates to the next level or the highscore view
			if(!LevelManager.Instance.LoadNextLevel()){
				NavigateToScene.GoToScene ("Highscore");
			}
		}

        //Todo: Add special effects and sound
        void collectItem(Item item)
        {
            //Debug.Log("+" + item.ItemValue + " Points!");
            data.Score += item.ItemValue;
            ParticleSpawner.Instance.SpawnParticleSystem(1, item.gameObject.transform);

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.playSound(0);
            }
            Destroy(item.gameObject);
        }

        public void onRun()
        {
            
            // Grab the current head transform and broadcast it to all the other users in the session
            Transform headTransform = Camera.main.transform;

            // Transform the head position and rotation into local space
            Vector3 headPosition = this.transform.InverseTransformPoint(headTransform.position);
            Quaternion headRotation = Quaternion.Inverse(this.transform.rotation) * headTransform.rotation;

            if (alive)
            {
                movement = speed * headRotation.z;
            }
            else
            {
                movement = 0;
            }

            playerObject.GetComponent<Rigidbody>().AddForce(new Vector3(movement, 0, 0));
            //playerObject.transform.position = new Vector3(transform.position.x - movement, transform.position.y, transform.position.z);
           
        }

        public void onJump()
        {
            playerObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));
            System.Diagnostics.Debug.WriteLine("DEBUG: Jump!");
        }

        void OnDestroy()
        {
            //Stores player data to the preferences
			data.Timer = Time.time - startTime;
            PreferenceManager.WriteJsonToPreferences("player", data);
        }

        // Use this for initialization
        void Start()
        {
            //TODO: Get PlayerName
			//data = PreferenceManager.ReadJsonFromPreferences<PlayerData>("player");
			//if (data == null) {
				data = new PlayerData("Name");
			//}
			startTime = Time.time;
			startPosition = playerObject.transform.position;
            alive = true;
            //GestureManager.Instance.OverrideFocusedObject = this.gameObject;

        }

        // Update is called once per frame
        void Update()
        {
            if (alive)
            {
                onRun();
            }
            else
            {
                // Grab the current head transform and broadcast it to all the other users in the session
                Transform headTransform = Camera.main.transform;

                // Transform the head position and rotation into local space
                Vector3 headPosition = this.transform.InverseTransformPoint(headTransform.position);
                Quaternion headRotation = Quaternion.Inverse(this.transform.rotation) * headTransform.rotation;

                if (headRotation.z > -1 || headRotation.z < 1)
                {
                    alive = true;
                }

            }
        }


    }

