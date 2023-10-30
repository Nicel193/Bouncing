using Audio;
using Game.Player;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private AudioSource _audioSourceGameMusic;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerSpawnPosition;
    
    public override void InstallBindings()
    {
        BindMusicPlayer();
        BindPlayer();
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
}
