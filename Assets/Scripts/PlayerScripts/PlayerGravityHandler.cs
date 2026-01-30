using Assets.Scripts.States.GameStates;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerGravityHandler
    {
        private readonly float _gravity;
        private readonly PlayerMover _mover;
        public bool IsGravityHandling { get; private set; }

        public PlayerGravityHandler(PlayerMover mover, float gravity)
        {
            _gravity = gravity;
            _mover = mover;
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
                    if (GameManager.Instance.IsPaused)
                        await UniTask.WaitUntil(() => !GameManager.Instance.IsPaused);
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
