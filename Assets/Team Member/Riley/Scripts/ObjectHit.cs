using System;
using System.Collections;
using System.Collections.Generic;
using RileyMcGowan;
using UnityEngine;

namespace RileyMcGowan
{
    public class ObjectHit : MonoBehaviour
    {
        #region Private Vars

        private FollowPath thisShip;

        #endregion

        #region Public Vars

        [Tooltip("Opponent ship ref.")] public FollowPath shipSecond;

        [Tooltip("Can the player currently hit each other?")]
        public bool hittable;

        #endregion

        private void Start()
        {
            thisShip = GetComponent<FollowPath>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == shipSecond.gameObject && hittable == true)
            {
                //Player hit other player
                shipSecond.speed -= 0.25f;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Rock"))
            {
                //Player hit rock
                thisShip.speed -= 0.5f;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("BoostPad"))
            {
                //Player hit boost pad
                thisShip.speed += 0.5f;
            }
        }
    }
}