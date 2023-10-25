using UnityEngine;


namespace Game.Player
{
    public class FollowPlayerCamera : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 5.0f;
        public float height = 15f;
        public float zOffset = 2.5f;
        
        private Vector3 offset;

        void Start()
        {
            offset = transform.position - target.position;
        }

        void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(
                new Vector3(transform.position.x, height, zOffset),
                new Vector3(desiredPosition.x, height, zOffset),
                smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}