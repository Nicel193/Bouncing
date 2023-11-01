using Audio;
using Map;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    using Player;
    
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Beat _beat;
        [SerializeField] private AudioSource _audioSourceGameMusic;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _playerSpawnPosition;

        public override void InstallBindings()
        {
            BindMusicPlayer();
            BindPlayer();
            BindBeat();
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
    }
}