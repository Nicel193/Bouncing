using Game.Configs;
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
        private void Construct(Player _player, Beat.Factory beatFactory, PlayerConfig playerConfig)
        {
            _chunkPlacer.Init(_player.transform, _chunk);
            _beatPlacer.Init(_player.transform, _chunk, beatFactory, playerConfig);
        }
        
        private void Update()
        {
            _chunkPlacer.Update();
            _beatPlacer.Update();
        }
    }
}