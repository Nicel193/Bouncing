using UnityEngine;

namespace Game.Player
{
    public class KeyboardInput : IPlayerInput
    {
        public float MovementDirection { get => _movementDirection; }

        private float _movementDirection = 1f;

        public void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _movementDirection = -_movementDirection;
        }
    }
}