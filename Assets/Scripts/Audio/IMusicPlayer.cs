namespace Audio
{
    public interface IMusicPlayer
    {
        void Play();
        void Stop();
        float GetCurrentTime();
    }
}