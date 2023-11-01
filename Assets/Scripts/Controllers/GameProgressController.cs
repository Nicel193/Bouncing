using Game.Views;
using UniRx;
using UnityEngine;

namespace Game.Controllers
{
    public class GameProgressController : IGameProgressController
    {
        private CompositeDisposable _disposable = new CompositeDisposable();

        private GameProgressModel _gameProgressModel;
        private IGameProgressView _gameProgressView;

        public GameProgressController(GameProgressView gameProgressView)
        {
            _gameProgressModel = new GameProgressModel();
            _gameProgressView = gameProgressView;

            _gameProgressModel.Score.Subscribe(OnScoreChanged).AddTo(_disposable);
            _gameProgressModel.LevelProgress.Subscribe(OnLevelProgressChanged).AddTo(_disposable);
        }

        public void AddScore()
        {
            _gameProgressModel.AddScore(1);
        }

        public void UpdateLevelProgress(float levelProgress)
        {
            levelProgress = Mathf.Clamp01(levelProgress);
            
            _gameProgressModel.SetLevelProgress(levelProgress);
        }

        private void OnScoreChanged(int score) => _gameProgressView.UpdateScore(score);
        private void OnLevelProgressChanged(float levelProgress) => _gameProgressView.UpdateLevelProgress(levelProgress);
    }
}