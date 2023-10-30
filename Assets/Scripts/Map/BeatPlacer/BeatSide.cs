using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class BeatSide
    {
        private bool _isLeftSide;
        private float _timeToChangeSide;
        private float _randomTime;
        private float _beatOffsetZ;
        private Chunk _chunk;

        public BeatSide(float beatOffsetZ, Chunk chunk)
        {
            _randomTime = Random.Range(2f, 5f);
            _beatOffsetZ = beatOffsetZ;
            _chunk = chunk;
        }
        
        public void Update()
        {
            _timeToChangeSide += Time.deltaTime;

            if (_timeToChangeSide >= _randomTime)
            {
                _timeToChangeSide = 0;
                _randomTime = Random.Range(2f, 5f);
                _isLeftSide = !_isLeftSide;
            }
        }

        public float GetSidePositionZ()
        {
            float positionZ = (_isLeftSide ? _chunk.LeftWallSide.position.z : _chunk.RightWallSide.position.z);
            return positionZ + (_isLeftSide? -_beatOffsetZ : _beatOffsetZ);
        }
    }
}