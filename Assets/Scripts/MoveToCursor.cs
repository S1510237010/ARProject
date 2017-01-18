using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Academy.HoloToolkit.Unity
{
    public class MoveToCursor : MonoBehaviour
    {

        private float startTime;
        public float duration;
        private Vector3 startPoint;
        private Vector3 endPoint;
        public bool follow;
        public GameObject mCursor;
        public float speed;
        public float speedUp;


        void Start()
        {

        }

        void FixedUpdate()
        {

            // Bewegung durch Rotation
            Transform TrCamera = Camera.main.transform;


            float distance = 0.001f;

            if (TrCamera.rotation.z < 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;

                transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
            }
            else if (TrCamera.rotation.z > 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.green;

                transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }


            /*
            startPoint = transform.position;
            
            if (true)
            {
                
                Vector3 cursorPoint = Camera.main.ScreenToWorldPoint(mCursor.transform.position);
                cursorPoint.z = startPoint.z;
                cursorPoint.y = startPoint.y;
                
                if (cursorPoint.x != transform.position.x)
                {
                    transform.position = Vector3.Lerp(startPoint, endPoint, (startTime - Time.time) / duration);
                    //transform.transform.up = GazeManager.Instance.Normal;
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(mCursor.transform.position);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        Vector3 endPoint = new Vector3(hit.point.x, startPoint.y, startPoint.z);
                        transform.position = Vector3.Lerp(startPoint, endPoint, (startTime - Time.time)/duration);
                    }
                } 

            }
            */
        }

    }
}