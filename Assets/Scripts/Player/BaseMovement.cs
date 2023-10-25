using UnityEngine;

namespace Game.Player
{
    public class BaseMovement : IMovement
    {
        private AudioSource _audioSource;
        private Rigidbody _playerRigidbody;
        private float _playerSpeed;
        private float _sphereRadius;

        public BaseMovement(AudioSource audioSource, Rigidbody playerRigidbody, float playerSpeed, float sphereRadius)
        {
            _audioSource = audioSource;
            _playerRigidbody = playerRigidbody;
            _playerSpeed = playerSpeed;
            _sphereRadius = sphereRadius;
        }

        public void Move(Vector3 direction)
        {
            direction.Normalize();
            
            Physics.Raycast(_playerRigidbody.position, new Vector3(0, 0, direction.z), out RaycastHit hit,
                5f);
            
            Vector3 collisionPoint = hit.point;
            Vector3 newPosition = new Vector3(_audioSource.time * _playerSpeed, 0,
                collisionPoint.z - (_sphereRadius * direction.z));
            
            float step = 15f * Time.fixedDeltaTime;
            Vector3 newwPosition = Vector3.MoveTowards(new Vector3(_audioSource.time * _playerSpeed, 0,_playerRigidbody.position.z), newPosition, step);
            
            _playerRigidbody.MovePosition(newwPosition);

            Debug.DrawRay(_playerRigidbody.position, new Vector3(0, 0, direction.z), Color.green);
        }
    }
}