using Game.Player;
using UnityEngine;
using Zenject;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Chunk _chunk;
        [SerializeField] private ChunkPlacer _chunkPlacer;
        [SerializeField] private BeatPlacer _beatPlacer;

        [Inject]
        private void Construct(Player _player)
        {
            _chunkPlacer.Init(_player.transform, _chunk);
            _beatPlacer.Init(_player.transform, _chunk);
        }
        
        private void Update()
        {
            _chunkPlacer.Update();
            _beatPlacer.Update();
        }
    }
}