namespace Game.Views
{
    public interface IGameProgressView
    {
        void UpdateScore(int score);
        void UpdateLevelProgress(float progress);
    }
}