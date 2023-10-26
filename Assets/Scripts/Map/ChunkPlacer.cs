using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Map
{
    public class ChunkPlacer : MonoBehaviour
    {
        private const int InitialChunkCount = 3;
        
        [SerializeField] private Chunk _chunk;
        [SerializeField] private Transform _player;
        [SerializeField] private float _spawnChunkOffsetX;

        private IObjectPool<Chunk> _chunkPull;
        private List<Chunk> _chunks = new List<Chunk>();

        private void Start()
        {
            _chunkPull = new GameObjectPull<Chunk>(_chunk, InitialChunkCount);

            SpawnFirstChunk();
            
            for (int i = 0; i < InitialChunkCount; i++) SpawnChunk();
        }

        private void Update()
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