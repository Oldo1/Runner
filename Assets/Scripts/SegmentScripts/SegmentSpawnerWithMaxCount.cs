using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.SegmentScripts
{
    public class SegmentSpawnerWithMaxCount : SegmentSpawner, ISegmentSpawnerAsync
    {
        private readonly int _maxSegmentSpawner;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isSpawning;
        private readonly GameManager _gameManager;

        public SegmentSpawnerWithMaxCount(GameObject[] segmentPrefab, float zOffset, int maxSegmentSpawner, GameManager gameManager) : base(segmentPrefab, zOffset)
        {
            _maxSegmentSpawner = maxSegmentSpawner;
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
                    if (_gameManager.IsPaused)
                        await UniTask.WaitUntil(() => !_gameManager.IsPaused, cancellationToken: token);
                    if (GameObjectPoolService.GetActiveCount() < _maxSegmentSpawner)
                        Spawn();
                    await UniTask.NextFrame(token);
                }
            }
            finally
            {
                _isSpawning = false;
            }
        }
    }
}