using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class FollowPlayerCamera : MonoBehaviour
    {
        private CameraConfig _cameraConfig;
        private Transform _playerTransform;
        private Vector3 _offset;

        [Inject]
        private void Construct(Player player, CameraConfig cameraConfig)
        {
            _playerTransform = player.transform;
            _cameraConfig = cameraConfig;
            _offset = transform.position - _playerTransform.position;
        }
        
        private void LateUpdate()
        {
            Vector3 desiredPosition = _playerTransform.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(
                new Vector3(transform.position.x, _cameraConfig.Height, _cameraConfig.ZOffset),
                new Vector3(desiredPosition.x, _cameraConfig.Height, _cameraConfig.ZOffset),
                _cameraConfig.SmoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}