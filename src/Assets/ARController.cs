using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{
    public GameObject robotObject;
    public ARRaycastManager RaycastManager;
    public int amountOfRobots = 5;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Amount of screen touches: " + Input.touchCount);
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> touches = new List<ARRaycastHit>();
                RaycastManager.Raycast(touch.position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                
                if (touches.Count > 0 && amountOfRobots > 0)
                {
                    amountOfRobots = amountOfRobots - 1;
                    GameObject.Instantiate(robotObject, touches[0].pose.position, touches[0].pose.rotation);
                }
            }
        }
    }
}
