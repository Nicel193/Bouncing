using UnityEngine;

namespace Game.Configs
{
    [System.Serializable]
    public class CameraConfig
    {
        [field: SerializeField] public float SmoothSpeed { get; private set; } = 5f;
        [field: SerializeField] public float Height { get; private set; } = 10f;
        [field: SerializeField] public float ZOffset { get; private set; } = -2.5f;
    }
}