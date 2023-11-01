using Audio;
using Game.Controllers;
using UnityEngine;
using Zenject;

namespace Map
{
    public class LevelLengthTracker : MonoBehaviour
    {
        private IGameProgressController _gameProgressController;
        private IMusicPlayer _musicPlayer;

        [Inject]
        private void Construct(IGameProgressController gameProgressController, IMusicPlayer musicPlayer)
        {
            _gameProgressController = gameProgressController;
            _musicPlayer = musicPlayer;
        }

        private void Update()
        {
            _gameProgressController.UpdateLevelProgress(_musicPlayer.GetCurrentTime() / _musicPlayer.GetFullTime());
        }
    }
}