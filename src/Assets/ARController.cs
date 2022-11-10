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
    public GameObject robotObjectNoAnimation;
    
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
                    GameObject savedRobot = GameObject.Instantiate(robotObjectNoAnimation, posePosition, poseRotation);
                    positions.Add(new RobotPosition(savedRobot, posePosition, poseRotation));
                } else if (touches.Count > 0)
                {
                    
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        var hitCollider = hit.collider;
                        var hitColliderGameObject = hitCollider.gameObject;
                        if (hitCollider.CompareTag("robot"))
                        {
                            RobotPosition robotPosition = positions.Find(item => item.gameObject.Equals(hitColliderGameObject));
                            GameObject.Instantiate(robotObject, robotPosition.posePosition, robotPosition.poseRotation);
                            GameObject.Destroy(hitColliderGameObject);
                        }
                    }
                    else
                    {
                        // Platziere Schild
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
