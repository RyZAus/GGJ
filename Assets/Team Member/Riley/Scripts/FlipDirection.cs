using UnityEngine;

namespace RileyMcGowan
{
    public class FlipDirection : MonoBehaviour
    {
        [Tooltip("If this object is an entry")]
        public bool entry;

        private void OnTriggerEnter(Collider other)
        {
            if (entry == true)
            {
                other.GetComponent<FollowPath>().flippedDirection = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (entry == false)
            {
                other.GetComponent<FollowPath>().flippedDirection = false;
            }
        }
    }
}
