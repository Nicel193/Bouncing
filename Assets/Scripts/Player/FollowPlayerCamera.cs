using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class FollowPlayerCamera : MonoBehaviour
    {
        [SerializeField] private float smoothSpeed = 5.0f;
        [SerializeField] private float height = 15f;
        [SerializeField] private float zOffset = 2.5f;
     
        private Transform _playerTransform;
        private Vector3 _offset;

        [Inject]
        private void Construct(Player player)
        {
            _playerTransform = player.transform;
            _offset = transform.position - _playerTransform.position;
        }
        
        private void LateUpdate()
        {
            Vector3 desiredPosition = _playerTransform.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(
                new Vector3(transform.position.x, height, zOffset),
                new Vector3(desiredPosition.x, height, zOffset),
                smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}