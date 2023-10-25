using UnityEngine;

namespace Map
{
    public class Chunk : MonoBehaviour
    {
        public Transform Begin;
        public Transform End;
        
        public AnimationCurve ChanceFromDistance;
    }
}