using UniRx;
using UnityEngine;

namespace Game
{
    public class GameProgressModel
    {
        public IReadOnlyReactiveProperty<int> Score => _score;
        public IReadOnlyReactiveProperty<float> LevelProgress => _levelProgress;
        
        private IntReactiveProperty _score;
        private FloatReactiveProperty _levelProgress;
        
        public GameProgressModel()
        {
            _score = new IntReactiveProperty();
            _levelProgress = new FloatReactiveProperty();
        }

        public void AddScore(int score) => _score.Value += score;
        public void SetLevelProgress(float progress) => _levelProgress.Value = progress;
        
    }
}