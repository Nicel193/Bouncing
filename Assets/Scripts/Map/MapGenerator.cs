using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private ChunkPlacer _chunkPlacer;
        [SerializeField] private BeatPlacer _beatPlacer;

        private void Start()
        {
            _chunkPlacer.Init(_player);
            _beatPlacer.Init(_player);
        }

        private void Update()
        {
            _chunkPlacer.Update();
            _beatPlacer.Update();
        }
    }
}