using System.Collections.Generic;
using Audio;
using Game.Player;
using ObjectPool;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class BeatPlacer
    {
        private const int InitBeatCount = 100;
        private const float SpawnOffsetPosition = 10f; 

        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private string _audioFilePath = "./Assets/test2.mp3";

        private List<double> _beatTimes = new List<double>();
        private List<GameObject> _beats = new List<GameObject>(InitBeatCount);
        private IObjectPool<GameObject> _beatsPool;

        private int _lastBeatIndex;
        private double _timeLoadOffset = 10f;
        private double _timePart;
        private Vector3 _playerStartPosition;
        private Transform _player;

        public void Init(Transform player)
        {
            IBeatDetector beatDetector = new BeatDetectorBase();
            _beatTimes = beatDetector.DetectBeats(_audioFilePath);
            _beatsPool = new GameObjectPool(GameObject.CreatePrimitive(PrimitiveType.Sphere), InitBeatCount);
            _player = player;
            _playerStartPosition = _player.position;
        }

        public void Update()
        {
            TrySpawnBeat();
            TryDeleteBeat();
        }

        /// <summary>
        ///  Spawn if offset player position is greater than next beat position
        /// </summary>
        private void TrySpawnBeat()
        {
            if(_lastBeatIndex >= _beatTimes.Count) return;

            if (_beatTimes[_lastBeatIndex] * _playerConfig.VerticalPlayerSpeed <= _player.position.x + SpawnOffsetPosition)
            {
                GameObject sphere = _beatsPool.Get();
                //S = V * T
                sphere.transform.position = new Vector3((float) _beatTimes[_lastBeatIndex] * _playerConfig.VerticalPlayerSpeed,
                    0, _playerStartPosition.z);

                _lastBeatIndex++;
                _beats.Add(sphere);
            }
        }

        private void TryDeleteBeat()
        {
            if(_beats.Count <= 0) return;

            if (_beats[0].transform.position.x < _player.position.x - SpawnOffsetPosition)
            {
                _beatsPool.Return(_beats[0]);
                _beats.RemoveAt(0);
            }
        }
    }
}