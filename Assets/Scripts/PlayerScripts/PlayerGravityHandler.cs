using Assets.Scripts.States.GameStates;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerGravityHandler
    {
        private readonly float _gravity;
        public bool IsGravityHandling { get; private set; }

        private readonly PlayerMover _mover;
        private readonly GameManager _gameManager;

        public PlayerGravityHandler(PlayerMover mover, float gravity, GameManager gameManager)
        {
            _gravity = gravity;
            _mover = mover;
            _gameManager = gameManager;
        }

        public async UniTask HandleGravity(CancellationToken token)
        {
            if (IsGravityHandling)
            {
                Debug.LogWarning("Gravity already handling");
                return;
            }
            IsGravityHandling = true;
            try
            {
                while (!_mover.IsOnGround)
                {
                    token.ThrowIfCancellationRequested();
                    if (_gameManager.IsPaused)
                        await UniTask.WaitUntil(() => !_gameManager .IsPaused);
                    _mover.Velocity -= _gravity * Time.deltaTime * Vector3.up;
                    await UniTask.NextFrame(token);
                }
            }
            finally
            {
                var newVelocity = _mover.Velocity;
                newVelocity.y = -0.5f;
                _mover.Velocity = newVelocity;
                IsGravityHandling = false;
            }
        }
    }
}
