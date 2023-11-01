using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/GameSettings", order = 0)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_cameraConfig);
            Container.BindInstance(_playerConfig);
        }
    }
}