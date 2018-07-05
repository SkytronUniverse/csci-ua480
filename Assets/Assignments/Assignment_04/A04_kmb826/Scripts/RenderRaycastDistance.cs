using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
 * Basic script to render info on-screen regarding RascastHit distance if the distance is within 15 units away.
 * 
 * This script is attached to: 
 *      Raycast Hit Distance Information Canvas
 */
namespace kmb_assignment04
{
    public class RenderRaycastDistance : MonoBehaviour
    {
        [SerializeField]
        private float distance;
        [SerializeField]
        private Text distanceText;

        void Start()
        {
            //Set rending of text initially to off
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }


        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag == "Plane" && hit.distance <= 15f) //Check if Raycast hits the plane, and also if it is within 15 units away
                {
                    //render the information on screen
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    distance = hit.distance;
                    distanceText.text = "D: " + distance.ToString("0.00");
                }

            }
            
            else
            {
                // if distance is greater than 15 units, stop rendering to screen
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
