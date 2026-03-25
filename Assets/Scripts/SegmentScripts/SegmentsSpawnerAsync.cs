using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsSpawnerAsync : SegmentSpawner
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isSpawning;

        private readonly float _spawnRate;
        private readonly GameManager _gameManager;

        public SegmentsSpawnerAsync(GameObject[] segmentsPrefabs, float zOffset, float spawnRate, GameManager gameManager) : base(segmentsPrefabs, zOffset)
        {
            _spawnRate = spawnRate;
            _gameManager = gameManager;
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
                    if (_gameManager)
                        await UniTask.WaitUntil(() => !_gameManager.IsPaused);
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