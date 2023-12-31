﻿using Audio;
using UnityEngine;

namespace Game.Player
{
    public class BaseMovement : IMovement
    {
        #region Consts

        private const float rayDistanse = 10f;
        private const float smoothMovment = 15f;

        #endregion

        public bool IsWalled { get; private set; }
        
        private IMusicPlayer _musicPlayer;
        private Transform _playerTransform;
        private float _playerVerticalSpeed;
        private float _playerHorizontalSpeed;
        private float _sphereRadius;
        
        private Vector3 _startPosition;
        
        public BaseMovement(IMusicPlayer musicPlayer, Transform playerRigidbody,
            float playerVerticalSpeed, float playerHorizontalSpeed, float sphereRadius)
        {
            _musicPlayer = musicPlayer;
            _playerTransform = playerRigidbody;
            _playerVerticalSpeed = playerVerticalSpeed;
            _playerHorizontalSpeed = playerHorizontalSpeed;
            _sphereRadius = sphereRadius;

            var position = _playerTransform.position;
            _startPosition = new Vector3(position.x + _musicPlayer.GetCurrentTime(), position.y, position.z);
        }

        
        //TODO: Change direction
        public void Move(Vector3 direction)
        {
            Vector3 newPosition = CalculateMoveForward();
            Vector3 correctPosition = CalculateChangeWall(newPosition, direction.z);

            _startPosition = _playerTransform.position = correctPosition;

#if UNITY_EDITOR
            // TestCalculateTime(_playerTransform.position.x);
            Debug.DrawRay(_playerTransform.position, new Vector3(0, 0, direction.z), Color.green);
#endif
        }

        private Vector3 CalculateMoveForward()
        {
            float audioTime = _musicPlayer.GetCurrentTime();
            float newPositionX = audioTime * _playerVerticalSpeed;

            Vector3 newPosition = Vector3.Lerp(_startPosition,
                new Vector3(newPositionX, _startPosition.y,
                    _startPosition.z),
                Time.deltaTime * smoothMovment);

            return newPosition;
        }

        private Vector3 CalculateChangeWall(Vector3 playerCurrentPosition, float wallDirection)
        {
            if (Physics.Raycast(_playerTransform.position, new Vector3(0, 0, wallDirection), out RaycastHit hit,
                rayDistanse))
            {
                Vector3 collisionPoint = hit.point;
                float edgePlayerPosition = collisionPoint.z - (_sphereRadius * wallDirection);
                Vector3 newPosition = new Vector3(playerCurrentPosition.x, playerCurrentPosition.y, edgePlayerPosition);
                
                IsWalled = playerCurrentPosition.z == edgePlayerPosition;
                float step = _playerHorizontalSpeed * Time.deltaTime;
                return Vector3.MoveTowards(playerCurrentPosition, newPosition, step);
            }

            return playerCurrentPosition;
        }

        private void TestCalculateTime(float pos)
        {
            float currentTimeInUnity = Time.time;
            float audioSourceTime = _musicPlayer.GetCurrentTime();

            Debug.Log(
                $"Player Position: {pos}, Unity Time: {currentTimeInUnity}, AudioSource Time: {audioSourceTime}");
        }
    }
}