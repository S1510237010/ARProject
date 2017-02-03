using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Academy.HoloToolkit.Unity
{
    public class Player : MonoBehaviour
    {
		private Vector3 startPosition;
        private PlayerData data;
        public GameObject playerObject;
        public float speed = 0.001f;
		private float startTime;

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

		void win(){
			//reset the deathcount and update the time
			data.DeathCount = 0;
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
            Transform TrCamera = Camera.main.transform;

            if (TrCamera.rotation.z < 0)
            {
                //gameObject.GetComponent<Renderer>().material.color = Color.red;

                playerObject.transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }
            else if (TrCamera.rotation.z > 0)
            {
                //gameObject.GetComponent<Renderer>().material.color = Color.green;

                playerObject.transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }
            else
            {
                //gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        public void onJump()
        {
            playerObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));
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

            //GestureManager.Instance.OverrideFocusedObject = this.gameObject;

        }

        void OnSelect()
        {
            onJump();
        }

    }
}
