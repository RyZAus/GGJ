using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class MovementPath : MonoBehaviour
    {
        #region Public Vars

        [Tooltip("List of all possible Waypoints")]
        public Transform[] pathSequence;

        #endregion

        private void OnDrawGizmos()
        {
            //If we don't have waypoints or don't have any connections, return
            if (pathSequence == null || pathSequence.Length < 2)
            {
                return;
            }

            //For however long the list of points is
            for (int i = 1; i < pathSequence.Length; i++)
            {
                //Draw lines for all connections
                Gizmos.DrawLine(pathSequence[i-1].position,pathSequence[i].position);
            }
            //Draw a line for the end of the loop to the start
            Gizmos.DrawLine(pathSequence[0].position, pathSequence[pathSequence.Length-1].position);
        }

        public IEnumerator<Transform> GetPathPoint(FollowPath currentPath)
        {
            //If we don't have any points, break
            if (pathSequence == null || pathSequence.Length < 1)
            {
                yield break;
            }

            while (true)
            {
                //After reaching this, it will GetPathPoint
                yield return pathSequence[currentPath.movingPoint];
                
                //If the length is 1, return cause it's at the end
                if (pathSequence.Length == 1)
                {
                    continue;
                }
                
                //Set direction forward (If we are point 1, we want 2)
                currentPath.movingPoint = currentPath.movingPoint + 1;
                
                //If the resulting number is too high, reset to 0
                if (currentPath.movingPoint >= pathSequence.Length)
                {
                    currentPath.lap += 1;
                    currentPath.movingPoint = 0;
                }
            }
        }
    }
}