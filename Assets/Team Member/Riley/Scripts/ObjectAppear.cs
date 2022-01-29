using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class ObjectAppear : MonoBehaviour
    {
        [Tooltip("What object to spawn here.")]
        public GameObject objectToSpawn;

        [Tooltip("How long to wait until spawning this object.")]
        public float durationTillSpawn = 0;

        private void Awake()
        {
            StartCoroutine(Timer(durationTillSpawn, objectToSpawn));
        }

        IEnumerator Timer(float timeToWait, GameObject spawnableObject)
        {
            yield return new WaitForSeconds(timeToWait);
            Instantiate(spawnableObject, transform.position, transform.rotation);
        }
    }
}