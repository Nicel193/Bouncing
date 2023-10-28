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
        private const int InitBeatCount = 30;
        private const float SpawnOffsetPosition = 10f; 

        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private string _audioFilePath = "./Assets/test2.mp3";
        [SerializeField] private float _beatOffsetZ;
        [SerializeField] private float _beatOffsetX;
        [SerializeField] private Beat _beat;

        private List<double> _beatTimes = new List<double>();
        private List<Beat> _beats = new List<Beat>(InitBeatCount);
        private IObjectPool<Beat> _beatsPool;

        private bool _isLeftSide;
        private int _lastBeatIndex;
        private Transform _player;
        private Chunk _chunk;

        private float _timeToChangeSide;
        private float _randomTime;

        public void Init(Transform player, Chunk chunk)
        {
            IBeatDetector beatDetector = new BeatDetectorBase();
            _beatTimes = beatDetector.DetectBeats(_audioFilePath);
            _beatsPool = new MonoObjectPool<Beat>(_beat, InitBeatCount);
            _player = player;
            _chunk = chunk;

            _randomTime = Random.Range(2f, 5f);
        }

        public void Update()
        {
            TrySpawnBeat();
            TryDeleteBeat();

            _timeToChangeSide += Time.deltaTime;

            if (_timeToChangeSide >= _randomTime)
            {
                _timeToChangeSide = 0;
                _randomTime = Random.Range(2f, 5f);
                _isLeftSide = !_isLeftSide;
            }
        }

        /// <summary>
        ///  Spawn if offset player position is greater than next beat position
        /// </summary>
        private void TrySpawnBeat()
        {
            if(_lastBeatIndex >= _beatTimes.Count) return;

            if (_beatTimes[_lastBeatIndex] * _playerConfig.VerticalPlayerSpeed <= _player.position.x + SpawnOffsetPosition)
            {
                Beat beat = _beatsPool.Get();
                float positionZ = (_isLeftSide ? _chunk.LeftWallSide.position.z : _chunk.RightWallSide.position.z); 
                //S = V * T
                beat.transform.position = new Vector3((float) _beatTimes[_lastBeatIndex] * _playerConfig.VerticalPlayerSpeed + _beatOffsetX,
                    0, positionZ + (_isLeftSide ? -_beatOffsetZ : _beatOffsetZ));
                
                _lastBeatIndex++;
                _beats.Add(beat);
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