using System;
using System.Collections.Generic;
using Audio;
using Game.Player;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private Transform _player;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private ChunkPlacer _chunkPlacer;
        [SerializeField] private string _audioFilePath = "./Assets/test2.mp3";
        
        private IBeatDetector _beatDetector;
        List<double> _beatTimes = new List<double>();
        
        private int _lastBeatIndex;
        private double _timeLoadOffset = 30f;
        private double _timePart;
        
        private void Start()
        {
            _chunkPlacer.Init(_player);
            _beatDetector = new BeatDetectorBase();
            _beatTimes = _beatDetector.DetectBeats(_audioFilePath);
        }

        private void Update()
        {
            _chunkPlacer.Update();
            
            if(_musicAudio.time >= _timePart - _timeLoadOffset) LoadBeats();
        }

        private void LoadBeats()
        {
            _timePart += _timeLoadOffset;
            
            for (int i = _lastBeatIndex; i < _beatTimes.Count; i++)
            {
                if ((float) _beatTimes[i] <= _timePart)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //S = V * T
                    sphere.transform.position = new Vector3((float) _beatTimes[i] * _playerConfig.VerticalPlayerSpeed,
                        0, _player.position.z);

                    _lastBeatIndex = i;
                    
                    break;
                }
            }
        }
    }
}