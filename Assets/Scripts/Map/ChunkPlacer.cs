using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class ChunkPlacer
    {
        private const int InitialChunkCount = 3;
        
        [SerializeField] private float _spawnChunkOffsetX = -10f;
        
        private Transform _player;
        private IObjectPool<Chunk> _chunkPull;
        private List<Chunk> _chunks = new List<Chunk>();

        public void Init(Transform player, Chunk chunk)
        {
            _player = player;
            _chunkPull = new MonoObjectPool<Chunk>(() => ObjectPoolCreators.Preload(chunk), InitialChunkCount);

            SpawnFirstChunk();
            
            for (int i = 0; i < InitialChunkCount; i++) SpawnChunk();
        }

        public void Update()
        {
            float distance = Vector3.Distance(_chunks[^1].End.position, _player.position);

            if (distance <= 10f)
            {
                DestroyChunk();
                SpawnChunk();
            }
        }

        private void SpawnFirstChunk()
        {
            Chunk newChunk = _chunkPull.Get();
            newChunk.transform.position = new Vector3(_spawnChunkOffsetX, 0, 0);
            _chunks.Add(newChunk);
        }
        
        private void SpawnChunk()
        {
            Chunk newChunk = _chunkPull.Get();
            newChunk.transform.position = _chunks[^1].End.position - newChunk.Begin.localPosition;
        
            _chunks.Add(newChunk);
        }
        
        private void DestroyChunk()
        {
            if (_chunks.Count > 0)
            {
                _chunkPull.Return(_chunks[0]);
                _chunks.RemoveAt(0);
            }
        }
    }
}