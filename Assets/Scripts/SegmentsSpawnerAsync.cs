using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsSpawnerAsync : SegmentSpawner, IService
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Transform _lastCreatedSegmentTransform;
        private GameObject[] _segmentsPrefabs;
        private readonly float _zOffset;
        private readonly float _spawnRate;
        private bool _isSpawning;

        public SegmentsSpawnerAsync(GameObject[] segmentsPrefabs, float zOffset, float spawnRate) : base(segmentsPrefabs, zOffset)
        {
            _segmentsPrefabs = segmentsPrefabs;
            _zOffset = zOffset;
            _spawnRate = spawnRate;
            foreach (var segmentPrefab in segmentsPrefabs)
            {
                GameObjectPoolService.CreatePool(segmentPrefab, initialCapacity: 20, maxSize: 30);
            }
            ServiceLocator.Register(this);
        }

        public void StartSpawning()
        {
            if (_isSpawning)
            {
                Debug.LogWarning("Obstacles already spawning");
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            SpawnAsync(_cancellationTokenSource.Token).Forget();
        }

        public void StopSpawning()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid SpawnAsync(CancellationToken token)
        {
            _isSpawning = true;
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    if (GameManager.Instance.IsPaused)
                        await UniTask.WaitUntil(() => !GameManager.Instance.IsPaused);
                    Spawn();
                    await UniTask.WaitForSeconds(_spawnRate, cancellationToken: token);
                }
            }
            finally
            {
                _isSpawning = false;
            }
        }
    }
}