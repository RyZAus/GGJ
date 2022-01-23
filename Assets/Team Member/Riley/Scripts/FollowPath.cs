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

        #endregion

        #region Private Vars

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
            if (Input.GetKeyDown(KeyCode.W))
            {
                int arrayIndexCurrent = Array.IndexOf(possiblePaths, currentPath);
                if (arrayIndexCurrent + 1 < possiblePaths.Length)
                {
                    ChangePath(possiblePaths[arrayIndexCurrent + 1]);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                int arrayIndexCurrent = Array.IndexOf(possiblePaths, currentPath);
                if (arrayIndexCurrent - 1 >= 0)
                {
                    ChangePath(possiblePaths[arrayIndexCurrent - 1]);
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

        private void SetupPath()
        {
            //Get our reference
            pointInPath = currentPath.GetPathPoint();

            //Get next point
            pointInPath.MoveNext();
        }

        private void ChangePath(MovementPath path)
        {
            int currentWaypoint = currentPath.movingTo;
            currentPath = path;
            currentPath.movingTo = currentWaypoint;
            SetupPath();
        }
    }
}