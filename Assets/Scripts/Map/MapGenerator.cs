using System;
using System.Collections.Generic;
using Audio;
using Game.Player;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private ChunkPlacer _chunkPlacer;
        [SerializeField] private string _audioFilePath = "./Assets/test2.mp3";
        
        private IBeatDetector _beatDetector;
        
        private void Start()
        {
            _chunkPlacer.Init(_player);
            
            IBeatDetector beatDetector = new BeatDetectorBase();
            List<double> beatTimes = beatDetector.DetectBeats(_audioFilePath);
            
            for (int i = 0; i < beatTimes.Count; i++)
            {
                Debug.Log(beatTimes[i]);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //S = V * T
                sphere.transform.position = new Vector3(((float)beatTimes[i] * _playerConfig.VerticalPlayerSpeed), 0, _player.position.z);
            }
        }

        private void Update()
        {
            _chunkPlacer.Update();
        }

        private void LoadBeats()
        {
            
        }
    }
}