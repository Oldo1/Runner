using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsSpawner
    {
        private CancellationTokenSource _cancellationTokenSource;
        private GameObject _lastCreatedSegment;
        private readonly GameObject[] _segmentsPrefabs;
        private readonly float _zOffset;
        private readonly float _spawnRate;

        public SegmentsSpawner(GameObject[] segmentsPrefabs, GameObject lastCreatedSegment, float zOffset, float spawnRate)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _segmentsPrefabs = segmentsPrefabs;
            _lastCreatedSegment = lastCreatedSegment;
            _zOffset = zOffset;
            _spawnRate = spawnRate;
            GameEvents.OnGameOver += StopSpawning;
            GameEvents.OnStartGame += StartSpawning;
        }

        public void StartSpawning()
        {
            Spawn(_cancellationTokenSource.Token).Forget();
        }

        public void StopSpawning()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid Spawn(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                var segmentPrefab = _segmentsPrefabs[UnityEngine.Random.Range(0, _segmentsPrefabs.Length)];
                var newSpawnPosition = _lastCreatedSegment.transform.position + Vector3.forward * _zOffset;
                var segment = GameObject.Instantiate(segmentPrefab, newSpawnPosition, Quaternion.identity);
                _lastCreatedSegment = segment;
                GameEvents.InvokeOnSpawnSegment(segment);
                await UniTask.WaitForSeconds(_spawnRate, cancellationToken: token);
            }
        }
    }
}