using System;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public AudioSource Audio;
        [SerializeField] private PlayerConfig _playerConfig;

        private IMovement _movement;
        private IPlayerInput _playerInput;

        private bool t;

        private void Awake() => Construct();

        private void Construct()
        {
            _movement = new BaseMovement(Audio, GetComponent<Rigidbody>(), _playerConfig.PlayerSpeed,
                GetComponent<SphereCollider>().radius);

            _playerInput = new KeyboardInput();
        }
        
        private void Update()
        {
            _playerInput.UpdateInput();
        }

        private void FixedUpdate()
        {
            _movement.Move(new Vector3(1, 0, _playerInput.MovementDirection));
        }
    }
}