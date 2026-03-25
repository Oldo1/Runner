using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsSpawner : SegmentSpawner
    {
        public SegmentsSpawner(GameObject[] segmentPrefab, float zOffset) : base(segmentPrefab, zOffset)
        {
        }

        public void Spawn(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be positive");
            for (int i = 0; i < count; i++)
            {
                Spawn();
            }
        }
    }
}