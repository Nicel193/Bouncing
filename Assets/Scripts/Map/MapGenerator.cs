using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Chunk _chunk;
        [SerializeField] private Transform _player;
        [SerializeField] private ChunkPlacer _chunkPlacer;
        [SerializeField] private BeatPlacer _beatPlacer;

        private void Start()
        {
            _chunkPlacer.Init(_player, _chunk);
            _beatPlacer.Init(_player, _chunk);
        }

        private void Update()
        {
            _chunkPlacer.Update();
            _beatPlacer.Update();
        }
    }
}