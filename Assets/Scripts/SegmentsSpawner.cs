using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsSpawner : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Transform _lastCreatedSegmentTransform;
        private GameObject[] _segmentsPrefabs;
        private float _zOffset;
        private float _spawnRate;
        private bool _isSpawning;

        public SegmentsSpawner(GameObject[] segmentsPrefabs, float zOffset, float spawnRate)
        {
            _segmentsPrefabs = segmentsPrefabs;
            _zOffset = zOffset;
            _spawnRate = spawnRate;
            GameEvents.OnGameOver += StopSpawning;
            GameEvents.OnStartGame += StartSpawning;
            GameEvents.OnPause += StopSpawning;
        }

        private void FindLastCreatedSegmentTransform()
        {
            var segments = GameObject.FindObjectsByType<Segment>(FindObjectsSortMode.None);
            var lastCreatedSegmemt = segments.OrderBy(x => Vector3.Distance(x.transform.position, Camera.main.transform.position)).Last();
            _lastCreatedSegmentTransform = lastCreatedSegmemt.transform;
        }

        public void StartSpawning()
        {
            if (_isSpawning)
            {
                Debug.LogWarning("Obstacles already spawning");
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            FindLastCreatedSegmentTransform();
            Spawn(_cancellationTokenSource.Token).Forget();
        }

        public void StopSpawning()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid Spawn(CancellationToken token)
        {
            _isSpawning = true;
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    var segmentPrefab = _segmentsPrefabs[UnityEngine.Random.Range(0, _segmentsPrefabs.Length)];
                    var newSpawnPosition = _lastCreatedSegmentTransform.position + Vector3.forward * _zOffset;
                    var segment = GameObject.Instantiate(segmentPrefab, newSpawnPosition, Quaternion.identity);
                    _lastCreatedSegmentTransform = segment.transform;
                    GameEvents.InvokeOnSpawnSegment(segment);
                    await UniTask.WaitForSeconds(_spawnRate, cancellationToken: token);
                }
            }
            finally
            {
                _isSpawning = false;
            }
        }

        public void Dispose()
        {
            GameEvents.OnGameOver -= StopSpawning;
            GameEvents.OnStartGame -= StartSpawning;
            GameEvents.OnPause -= StopSpawning;
        }
    }
}