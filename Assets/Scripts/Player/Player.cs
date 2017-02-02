using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Academy.HoloToolkit.Unity
{
    public class Player : MonoBehaviour
    {
        private PlayerData data;
        public GameObject playerObject;
        public float speed = 0.001f;

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
                collectItem(other.GetComponent<Item>());
            }

            if (other.tag == "spikes")
            {
                //TODO: respawn the character/reset the scene
                die();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "border")
            {
                //TODO: respawn the character/reset the scene
                die();
            }
        }

        //Todo: Add respawn and sound
        void die()
        {
            Debug.Log("Dead");

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.playSoundAt(1, gameObject.transform);
            }

            int duration = ParticleSpawner.Instance.SpawnParticleSystem(0, gameObject.transform);
            Destroy(gameObject);
        }

        //Todo: Add special effects and sound
        void collectItem(Item item)
        {
            Debug.Log("+" + item.ItemValue + " Points!");
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
            //PreferenceManager.WriteJsonToPreferences("player", data);
        }

        // Use this for initialization
        void Start()
        {
            //TODO: Get PlayerName

            data = new PlayerData();
            data.PlayerName = "Name";
            data.Score = 0;


            GestureManager.Instance.OverrideFocusedObject = this.gameObject;

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnSelect()
        {
            onJump();
        }

    }
}
