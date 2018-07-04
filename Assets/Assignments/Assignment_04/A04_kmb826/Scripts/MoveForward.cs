using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kmb_assignment04
{
    public class MoveForward : MonoBehaviour
    {
        private const float MAXSPEED = 4f;
        private const float teleportY = 1.5f;

        private float rayDistance;
        private Vector3 rayPoint;
        private static float speed = 0.001f;

        private static bool buttonDown = false;
        private static bool rayHit = false;

        private Vector3 moveDirect;

        private void Update()
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)) {
                print("Object Hit: " + hit.transform.name);
                if (hit.transform.name == "Plane")
                {
                    print("Ray Distance: " + hit.distance);
                    print("Hit Info: " + hit.point);
                    rayDistance = hit.distance;
                    rayPoint = hit.point;
                    rayPoint.y = teleportY;
                    rayHit = true;
                }
            } else
            {
                rayHit = false;
            }

            if (Input.GetMouseButton(0) && (!rayHit || rayDistance >= 10f))
            {
                Debug.Log("Button Down!");
                buttonDown = true;
            } else if (Input.GetMouseButton(0) && (rayHit && rayDistance < 10f))
            {
                buttonDown = false;
                transform.position = rayPoint;
            }
            else
            {
                buttonDown = false;
                rayHit = false;
            }
           
        }
      

            
        public void FixedUpdate()
        {

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
                    speed += 0.015f;
                    Debug.Log("Speed:" + speed);
                }
                else
                {
                    moveDirect = Camera.main.transform.forward;
                    moveDirect.y = 0f;
                    transform.Translate(moveDirect * MAXSPEED);
                    Debug.Log("Speed:" + speed);
                }

            } else
            {
                
                if(speed >=0)
                {
                    moveDirect = Camera.main.transform.forward;
                    moveDirect.y = 0f;
                    transform.Translate(moveDirect * speed);
                    speed -= 0.015f;
                    Debug.Log("Slow Down Speed: " + speed);
                }

                buttonDown = false;

            }
        }


    }
    
}
