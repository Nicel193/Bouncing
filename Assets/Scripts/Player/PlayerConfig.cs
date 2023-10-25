﻿using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }

        [field: Header("Camera")]
        [field: SerializeField] public float SmoothSpeed { get; private set; } = 5f;
        [field: SerializeField] public float Height { get; private set; } = 10f;
    }
}