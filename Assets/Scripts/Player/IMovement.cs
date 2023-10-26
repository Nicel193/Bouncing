using UnityEngine;

namespace Game.Player
{
    public interface IMovement
    {
        bool IsWalled { get; }

        void Move(Vector3 direction);
    }
}