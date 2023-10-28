using System;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private const float MoveForwardDirection = 1;
        
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private PlayerConfig _playerConfig;

        private IMovement _movement;
        private IPlayerInput _playerInput; 
        
        private void Start() => Construct();
        
        private void Construct()
        {
            _movement = new BaseMovement(_musicAudioSource, 
                this.transform, 
                _playerConfig.VerticalPlayerSpeed,
                _playerConfig.HorizontalPlayerSpeed,
                GetComponent<SphereCollider>().radius);

            _playerInput = new KeyboardInput();
        }

        private void Update()
        {
            if(_movement.IsWalled) _playerInput.UpdateInput();
            
            _movement.Move(new Vector3(MoveForwardDirection, 0, _playerInput.MovementDirection));
        }
    }
}