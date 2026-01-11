using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerGravityHandler
    {
        private readonly float _gravity;
        private readonly PlayerMover _mover;
        private bool _isGravityHandling;

        public PlayerGravityHandler(PlayerMover mover, float gravity)
        {
            _gravity = gravity;
            _mover = mover;
        }

        public async UniTask HandleGravity(CancellationToken token)
        {
            if (_isGravityHandling)
            {
                Debug.LogWarning("Gravity already handling");
                return;
            }
            _isGravityHandling = true;

            try
            {
                while (!_mover.IsOnGround)
                {
                    token.ThrowIfCancellationRequested();
                    _mover.Velocity -= _gravity * Time.deltaTime * Vector3.up;
                    await UniTask.NextFrame(token);
                }
                var newVelocity = _mover.Velocity;
                newVelocity.y = -0.5f;
                _mover.Velocity = newVelocity;
            }
            finally
            {
                _isGravityHandling = false;
            }
        }
    }
}
