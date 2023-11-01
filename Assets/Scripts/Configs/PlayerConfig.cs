using UnityEngine;

namespace Game.Configs
{
    [System.Serializable]
    public class PlayerConfig
    {
        [field: SerializeField] public float VerticalPlayerSpeed { get; private set; } = 3f;
        [field: SerializeField] public float HorizontalPlayerSpeed { get; private set; } = 15f;
    }
}