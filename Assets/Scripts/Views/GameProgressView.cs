using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class GameProgressView : MonoBehaviour, IGameProgressView
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image _levelProgressBar;
        
        public void UpdateScore(int score)
        {
            if(score < 0) return;
            
            _scoreText.text = $"Score: {score}";
        }

        public void UpdateLevelProgress(float progress)
        {
            _levelProgressBar.fillAmount = progress;
        }
    }
}