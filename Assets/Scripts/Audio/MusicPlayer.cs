using UnityEngine;

namespace Audio
{
    public class MusicPlayer : IMusicPlayer
    {
        private AudioSource _audioSource;

        public MusicPlayer(AudioSource audioSource) =>
            _audioSource = audioSource;

        public void Play() => _audioSource.Play();
        public void Stop() => _audioSource.Stop();
        public float GetCurrentTime() => _audioSource.time;
    }
}