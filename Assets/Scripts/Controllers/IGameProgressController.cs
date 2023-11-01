namespace Game.Controllers
{
    public interface IGameProgressController
    {
        public void AddScore();
        public void UpdateLevelProgress(float levelProgress);
    }
}