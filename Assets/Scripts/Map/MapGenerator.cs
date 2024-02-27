using Audio;
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
        private void Construct(Player _player, Beat.Factory beatFactory, PlayerConfig playerConfig,
            IMusicPlayer musicPlayer)
        {
            string audioPath = GetAudioPath(musicPlayer);
            
            _chunkPlacer.Init(_player.transform, _chunk);
            _beatPlacer.Init(_player.transform, _chunk, beatFactory, playerConfig, audioPath);
        }

        private static string GetAudioPath(IMusicPlayer musicPlayer)
        {
            string audioPath = "./Assets/Resources/" + Paths.Music + "/" + musicPlayer.GetMusicName() + ".mp3";
            
            return audioPath;
        }

        private void Update()
        {
            _chunkPlacer.Update();
            _beatPlacer.Update();
        }
    }
}