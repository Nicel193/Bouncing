using Unity.VisualScripting;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerInput
    {
        float MovementDirection { get; }

        void UpdateInput();
    }
}