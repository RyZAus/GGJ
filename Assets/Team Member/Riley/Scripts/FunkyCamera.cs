using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class FunkyCamera : MonoBehaviour
    {
        #region Private Vars
        //Is the camera updated
        private bool isRunning;
        
        //Main ship to follow
        private GameObject firstPlaceShip;
        
        //How long should the camera take to reach it's target
        [SerializeField] [Range(0.01f, 1f)] private float smoothTime = 0.125f;
        
        //What is the current velocity
        private Vector3 cameraVelocity = Vector3.zero;
        
        //Where should the camera sit
        [SerializeField] private Vector3 offset = Vector3.zero;
        #endregion

        #region Public Vars
        public FollowPath[] ships;
        #endregion

        #region Core Functions
        private void Update()
        {
            if (isRunning != true)
            {
                if (ships.Length > 1)
                {
                    int lapCompair = ships[0].lap - ships[1].lap;
                    float percentCompair = ships[0].percentCompleted - ships[1].percentCompleted;

                    //TODO Revisit
                    
                    //Tried a switch here to make it cleaner but for some reason
                    //unity refused to let me use greater/less than
                    if (lapCompair > 0)
                    {
                        firstPlaceShip = ships[0].gameObject;
                    }
                    else if (lapCompair < 0)
                    {
                        firstPlaceShip = ships[1].gameObject;
                    }
                    else
                    {
                        if (percentCompair > 0)
                        {
                            firstPlaceShip = ships[0].gameObject;
                        }
                        else if (percentCompair < 0)
                        {
                            firstPlaceShip = ships[1].gameObject;
                        }
                    }
                }
                //Stop the camera running constantly
                StartCoroutine(CameraTimer());
            }
        }

        private void LateUpdate()
        {
            Vector3 destination = firstPlaceShip.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref cameraVelocity, smoothTime);
        }
        #endregion
        

        #region CoRoutines
        IEnumerator CameraTimer()
        {
            //Check if running
            isRunning = true;
            //Then wait
            yield return new WaitForSeconds(0.5f);
            isRunning = false;
        }
        #endregion
    }
}