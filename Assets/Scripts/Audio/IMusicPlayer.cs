namespace Audio
{
    public interface IMusicPlayer
    {
        void Play();
        void Stop();
        float GetCurrentTime();
        float GetFullTime();
        string GetMusicName();
    }
}