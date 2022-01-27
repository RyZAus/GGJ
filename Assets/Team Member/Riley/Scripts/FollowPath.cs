using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RileyMcGowan
{
    public class FollowPath : MonoBehaviour
    {
        #region Public Vars

        [Tooltip("Selected path allows lanes")]
        public MovementPath currentPath;

        [Tooltip("All possible paths")]
        public MovementPath[] possiblePaths;

        [Tooltip("Current speed of vehicle")] public float speed = 1;

        [Tooltip("How close to the waypoint to get")]
        public float maxDistanceToGoal = .1f;

        [Tooltip("Allows us to correct the direction you are swapping")]
        public bool flippedDirection;
        
        [Tooltip("Prevents swapping over unswappable sections")]
        public bool unableToSwap;
        
        [Tooltip("Is this player 1 controls")]
        public bool player1Controls;

        #endregion

        #region Private Vars

        public int movingPoint = 0;
        private IEnumerator<Transform> pointInPath; //Gets the reference to the path point fed by MovementPath

        #endregion

        private void Start()
        {
            //Error if no path
            if (currentPath == null)
            {
                Debug.LogError("No Path - F1", gameObject);
                return;
            }

            SetupPath();

            //Error if no path
            if (pointInPath.Current == null)
            {
                Debug.LogError("No Path - F2", gameObject);
                return;
            }

            //Set our initial position
            transform.position = pointInPath.Current.position;
        }

        private void Update()
        {
            if (player1Controls == true)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    ChangePathUp();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    ChangePathDown();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    ChangePathUp();
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    ChangePathDown();
                }
            }
            
            //If null, error
            if (pointInPath == null || pointInPath.Current == null)
            {
                Debug.LogError("No Path - F3", gameObject);
                return;
            }

            //Take the position, and the current target position, then use speed with time to move
            transform.position =
                Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
            var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
            if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
            {
                pointInPath.MoveNext();
            }
        }

        #region Public Functions
        public void ChangePathUp() //CALL THIS UNLESS OTHERWISE ASKED
        {
            if (unableToSwap != true)
            {
                if (flippedDirection)
                {
                    ChangePathDownForced();
                }
                else
                {
                    ChangePathUpForced();
                }
            }
        }
        public void ChangePathDown() //CALL THIS UNLESS OTHERWISE ASKED
        {
            if (unableToSwap != true)
            {
                if (flippedDirection)
                {
                    ChangePathUpForced();
                }
                else
                {
                    ChangePathDownForced();
                }
            }
        }
        
        public void ChangePathUpForced()
        {
            int arrayIndexCurrent = Array.IndexOf(possiblePaths, currentPath);
            if (arrayIndexCurrent + 1 < possiblePaths.Length)
            {
                ChangePathSetup(possiblePaths[arrayIndexCurrent + 1]);
            }
        }

        public void ChangePathDownForced()
        {
            int arrayIndexCurrent = Array.IndexOf(possiblePaths, currentPath);
            if (arrayIndexCurrent - 1 >= 0)
            {
                ChangePathSetup(possiblePaths[arrayIndexCurrent - 1]);
            }
        }
        #endregion

        #region Private Functions
        private void SetupPath()
        {
            //Get our reference
            pointInPath = currentPath.GetPathPoint(this);
            
            //Get next point
            pointInPath.MoveNext();
        }

        private void ChangePathSetup(MovementPath path)
        {
            currentPath = path;
            SetupPath();
        }
        #endregion
    }
}
