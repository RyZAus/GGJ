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

        [Tooltip("Is this a non player object")]
        public bool objectNonPlayer;
        
        public int movingPoint = 0;
        public float percentCompleted;
        public int lap = 0;
        #endregion

        #region Private Vars

        private float percentPerWaypoint;
        private ObjectHit thisObjectsHit;
        private float percentDistance;
        private IEnumerator<Transform> pointInPath; //Gets the reference to the path point fed by MovementPath

        #endregion

        private void Start()
        {
            //Error if no path
            if (currentPath == null && objectNonPlayer != true)
            {
                Debug.LogError("No Path - F1", gameObject);
                return;
            }
            else if (objectNonPlayer == true)
            {
                PathType[] paths = FindObjectsOfType<PathType>();
                foreach (PathType path in paths)
                {
                    if (path.thisPath == PathType.TypesOfPaths.RockPath)
                    {
                        possiblePaths[0] = path.GetComponent<MovementPath>();
                        currentPath = path.GetComponent<MovementPath>();
                    }
                }
            }

            SetupPath();

            //Error if no path
            if (pointInPath.Current == null)
            {
                Debug.LogError("No Path - F2", gameObject);
                return;
            }

            if (GetComponent<ObjectHit>() != null)
            {
                thisObjectsHit = GetComponent<ObjectHit>();
            }
            else if (objectNonPlayer == false)
            {
                Debug.LogError(this + " does not contain ObjectHit!");
            }

            //Split the percentage between each waypoint
            percentPerWaypoint = 100f / currentPath.pathSequence.Length;
            
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
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ChangePathUp();
                }
            }
            
            //If null, error
            if (pointInPath == null || pointInPath.Current == null)
            {
                Debug.LogError("No Path - F3", gameObject);
                return;
            }

            //Take the position, and the current target position, then use speed with time to move
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
            transform.LookAt(pointInPath.Current.position);
            
            //Get a distance between the ship and the next waypoint
            var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
            
            //Use the distance and compair the full distance for a percentage of the completed waypoint
            percentCompleted = (percentPerWaypoint * movingPoint) + ((1 - (distanceSquared/percentDistance)) * percentPerWaypoint);
            
            //If the distance has reached the waypoint move on
            if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
            {
                pointInPath.MoveNext();
                percentDistance = (transform.position - pointInPath.Current.position).sqrMagnitude;
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
                if (thisObjectsHit.hittable != true)
                {
                    StartCoroutine(HitPlayerTimer());
                }
                ChangePathSetup(possiblePaths[arrayIndexCurrent + 1]);
            }
            else
            {
                ChangePathDownForced()
            }
        }

        public void ChangePathDownForced()
        {
            int arrayIndexCurrent = Array.IndexOf(possiblePaths, currentPath);
            if (arrayIndexCurrent - 1 >= 0)
            {
                if (thisObjectsHit.hittable != true)
                {
                    StartCoroutine(HitPlayerTimer());
                }
                ChangePathSetup(possiblePaths[arrayIndexCurrent - 1]);
            }
            else
            {
                ChangePathUpForced();
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
            percentDistance = (transform.position - pointInPath.Current.position).sqrMagnitude;
        }

        private void ChangePathSetup(MovementPath path)
        {
            currentPath = path;
            SetupPath();
        }
        #endregion

        #region CoRoutines
        IEnumerator HitPlayerTimer()
        {
            thisObjectsHit.hittable = true;
            yield return new WaitForSeconds(1f);
            thisObjectsHit.hittable = false;
        }
        #endregion
    }
}
