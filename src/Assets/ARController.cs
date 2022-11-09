using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{
    public GameObject robotObject;
    public ARRaycastManager RaycastManager;
    public int amountOfRobots = 5;
    private List<RobotPosition> positions = new List<RobotPosition>();
    
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
                    Vector3 posePosition = touches[0].pose.position;
                    Quaternion poseRotation = touches[0].pose.rotation;
                    GameObject savedRobot = GameObject.Instantiate(robotObject, posePosition, poseRotation);
                    positions.Add(new RobotPosition(savedRobot, posePosition, poseRotation));
                } else if (touches.Count > 0)
                {

                    RobotPosition robot = positions[positions.Count - 1];
                    positions.Remove(robot);
                    if (robot != null)
                    {
                        GameObject.Destroy(robot.gameObject);
                    }
                }
            }
        }
    }

    class RobotPosition
    {
        public GameObject gameObject;
        public Vector3 posePosition;
        public Quaternion poseRotation;

        public RobotPosition(GameObject gameObject, Vector3 posePosition, Quaternion poseRotation)
        {
            this.gameObject = gameObject;
            this.posePosition = posePosition;
            this.poseRotation = poseRotation;
        }
    }
}
