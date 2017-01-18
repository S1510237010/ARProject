using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCursor : MonoBehaviour {

    private float startTime;
    public Transform CursorTransform;
    public float duration;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public Camera camera;


    void Start()
    {
        startTime = Time.time;
        startPoint = transform.position;

        if (duration == 0)
        {
            duration = 1.0f;
            Debug.Log("Error: duration cant be 0 ... duration set to one");
        }

    }

    void FixedUpdate()
    { 
        Vector3 cursorPoint = camera.ScreenToWorldPoint(CursorTransform.position);

        endPoint = new Vector3(cursorPoint.x + 10, startPoint.y, startPoint.z);

        transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
    }
}