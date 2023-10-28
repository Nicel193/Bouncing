using UnityEngine;

namespace Map
{
    public class Chunk : MonoBehaviour
    {
        [field: SerializeField] public Transform Begin { get; private set; }
        [field: SerializeField] public Transform End { get; private set; }
        [field: SerializeField] public Transform LeftWallSide { get; private set; }
        [field: SerializeField] public Transform RightWallSide { get; private set; }
    }
}