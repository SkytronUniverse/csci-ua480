using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script makes it possible for the player to fly around and teleport.
 * 
 * This script is attached to:
 *      Player
 */ 
namespace kmb_assignment04
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField]
        private const float MAXSPEED = 3.0f; // maximum speed allowed
        [SerializeField]
        private const float teleportY = 1.5f; // if teleport, we always want the player y-position to remain the same

        [SerializeField]
        private float rayDistance; //stores the distance of a RayCastHit that will be used for teleportation
        [SerializeField]
        private Vector3 rayPoint; //stores the coordinates of a RayCastHit that will be used for teleportation
        [SerializeField]
        private static float speed = 0f; //beginning speed to be used for acceleration

        [SerializeField]
        private static bool buttonDown = false; // boolean variable set to true if button is being pressed down
        [SerializeField]
        private static bool rayHit = false; //boolean variable set to true if there is a RayCastHit on the plane

        [SerializeField]
        private Vector3 moveDirect; //move direction used for flying around the scene

        private void Update()
        {
            RaycastHit hit;

            //Get information about RayCastHit
            //If Raycast hits the plane, then distance, coordinates of hit will be collected in case of teleportation
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)) {
                print("Object Hit: " + hit.transform.name);
                if (hit.transform.tag == "Plane")
                {
                    print("Ray Distance: " + hit.distance); // for debugging
                    print("Hit Info: " + hit.point); // for debugging
                    rayDistance = hit.distance; 
                    rayPoint = hit.point;
                    rayPoint.y = teleportY;
                    rayHit = true;
                }
            } else
            {
                //No RaycastHit occurs
                rayHit = false;
            }

            // Check for button pressed, and if there is no Raycast Hit or, if there is a hit, checks to see if it is greater than or equal to 10m away
            if (Input.GetMouseButton(0) && (!rayHit || rayDistance >= 10f))
            {
                Debug.Log("Button Down!");
                buttonDown = true;
            }
            //If previous evaluation fails, then checks to see if teleportation is necessary
            else if (Input.GetMouseButton(0) && (rayHit && rayDistance < 10f))
            {
                buttonDown = false;
                transform.position = rayPoint; //teleport to the point where the Raycast hit on the plane
            }
            // Otherwise do nothing and make sure boolean variables are set to false
            else
            {
                buttonDown = false;
                rayHit = false;
            }
           
        }
      
        // Fixed update manages execution of flying with acceleration
        public void FixedUpdate()
        {
            //Fly with acceleration
            if(buttonDown)
            {
                moveDirect = Camera.main.transform.forward;
                if (speed <= MAXSPEED)
                {
                    Debug.Log("Player y = " + transform.position.y);
                    if (transform.position.y < 1f) //Make sure that player does not go belox the playing area
                        if (moveDirect.y >= 0f)
                            moveDirect = Camera.main.transform.forward;
                        else
                            moveDirect.y = 0f; //If player y-position is < 1 then keep the y-position at 0 with regard to the moving direction
                    transform.Translate( moveDirect* speed);
                    speed += 0.005f; // increase speed to simulate acceleration
                    Debug.Log("Speed:" + speed); // for debugging
                }
                //If speed >= MAXSPEED, then just fly at maximum speed
                else
                {
                    moveDirect = Camera.main.transform.forward;
                    moveDirect.y = 0f;
                    transform.Translate(moveDirect * MAXSPEED);
                    Debug.Log("Speed:" + speed); // for debugging
                }

            } else
            {
                //Decelerate if button is no longer being held down.
                if(speed >=0)
                {
                    moveDirect = Camera.main.transform.forward;
                    moveDirect.y = 0f;
                    transform.Translate(moveDirect * speed);
                    speed -= 0.015f;
                    Debug.Log("Slow Down Speed: " + speed); // for debugging
                }

                buttonDown = false;

            }
        }


    }
    
}
