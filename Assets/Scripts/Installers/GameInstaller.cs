using Audio;
using Map;
using UnityEngine;
using Zenject;

using Game.Controllers;
using Game.Views;

namespace Game.Installers
{
    using Player;
    
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameProgressView _gameProgressView;
        [SerializeField] private Beat _beat;
        [SerializeField] private AudioSource _audioSourceGameMusic;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _playerSpawnPosition;
        
        public override void InstallBindings()
        {
            BindMusicPlayer();
            BindPlayer();
            BindBeat();
            BindProgressController();
        }
        
        private void BindMusicPlayer()
        {
            Container.Bind<IMusicPlayer>()
                .To<MusicPlayer>()
                .AsSingle()
                .WithArguments(_audioSourceGameMusic);
        }

        private void BindPlayer()
        {
            var player = Container
                .InstantiatePrefabForComponent<Player>(_player, _playerSpawnPosition);

            Container.Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }

        private void BindBeat()
        {
            Container.BindFactory<Beat, Beat.Factory>()
                .FromComponentInNewPrefab(_beat)
                .UnderTransformGroup("Beats");
        }

        private void BindProgressController()
        {
            Container.Bind<IGameProgressController>()
                .To<GameProgressController>()
                .AsSingle()
                .WithArguments(_gameProgressView);
        }
    }
}