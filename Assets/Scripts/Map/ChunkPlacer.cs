﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class ChunkPlacer : MonoBehaviour
    {
        public Transform Player;
        public Chunk[] ChunkPrefabs;
        public Chunk FirstChunk;

        private List<Chunk> spawnedChunks = new List<Chunk>();

        private void Start()
        {
            spawnedChunks.Add(FirstChunk);
            SpawnChunk();
        }

        private void Update()
        {
            float distance = Vector3.Distance(spawnedChunks[spawnedChunks.Count - 1].End.position, Player.position);
            
            if (distance <= -10f) SpawnChunk();
        }

        private void SpawnChunk()
        {
            Chunk newChunk = Instantiate(GetRandomChunk());
            newChunk.transform.position =
                spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
            spawnedChunks.Add(newChunk);

            if (spawnedChunks.Count >= 3)
            {
                Destroy(spawnedChunks[0].gameObject);
                spawnedChunks.RemoveAt(0);
            }
        }

        private Chunk GetRandomChunk()
        {
            // List<float> chances = new List<float>();
            // for (int i = 0; i < ChunkPrefabs.Length; i++)
            // {
            //     chances.Add(ChunkPrefabs[i].ChanceFromDistance.Evaluate(Player.transform.position.z));
            // }
            //
            // float value = Random.Range(0, chances.Sum());
            // float sum = 0;
            //
            // for (int i = 0; i < chances.Count; i++)
            // {
            //     sum += chances[i];
            //     if (value < sum)
            //     {
            //         return ChunkPrefabs[i];
            //     }
            // }

            return ChunkPrefabs[0];
        }
    }
}