﻿using System.Collections.Generic;
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
        
        private int _lastBeatIndex;
        private Transform _player;
        private BeatSide _beatSide;

        public void Init(Transform player, Chunk chunk)
        {
            IBeatDetector beatDetector = new BeatDetectorBase();
            _beatsPool = new MonoObjectPool<Beat>(_beat, InitBeatCount);
            _beatSide = new BeatSide(_beatOffsetZ, chunk);
            _beatTimes = beatDetector.DetectBeats(_audioFilePath);
            _player = player;
        }

        public void Update()
        {
            TrySpawnBeat();
            TryDeleteBeat();
            
            _beatSide.Update();
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
                //S = V * T
                beat.transform.position = new Vector3((float) _beatTimes[_lastBeatIndex] * _playerConfig.VerticalPlayerSpeed + _beatOffsetX,
                    0, _beatSide.GetSidePositionZ());
                
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