using System;
using UnityEngine;

namespace Game.Player
{
    // [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private PlayerConfig _playerConfig;

        private IMovement _movement;
        private IPlayerInput _playerInput; 
        float timeDifference = 0.0f;

        // private void Awake() => Construct();

        private void Start()
        {
            _musicAudioSource.Play();
            Construct();
        }

        private void Construct()
        {
            _movement = new BaseMovement(_musicAudioSource, 
                this.transform, 
                _playerConfig.VerticalPlayerSpeed,
                _playerConfig.HorizontalPlayerSpeed,
                GetComponent<SphereCollider>().radius);

            _playerInput = new KeyboardInput();
        }

        private float t;
        
        private void Update()
        {
            _playerInput.UpdateInput();
            _movement.Move(new Vector3(1, 0, _playerInput.MovementDirection));
            // _movement.Move(new Vector3(1, 0, _playerInput.MovementDirection));
        }

        private void FixedUpdate()
        {

        }
    }
}