using Audio;
using UnityEngine;
using Zenject;
using Game.Configs;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Player : MonoBehaviour
    {
        private const float MoveForwardDirection = 1;

        private IMovement _movement;
        private IPlayerInput _playerInput; 
        
        [Inject]
        private void Construct(IMusicPlayer musicPlayer, PlayerConfig playerConfig)
        {
            _movement = new BaseMovement(musicPlayer, 
                this.transform, 
                playerConfig.VerticalPlayerSpeed,
                playerConfig.HorizontalPlayerSpeed,
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