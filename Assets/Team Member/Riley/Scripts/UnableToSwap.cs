using UnityEngine;

namespace RileyMcGowan
{
    public class UnableToSwap : MonoBehaviour
    {
        [Tooltip("If this object is an entry")]
        public bool entry;

        private void OnTriggerEnter(Collider other)
        {
            if (entry == true)
            {
                other.GetComponent<FollowPath>().unableToSwap = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (entry == false)
            {
                other.GetComponent<FollowPath>().unableToSwap = false;
            }
        }
    }
}
