using Assets.Scripts.States.GameStates;
using Assets.Scripts;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class PlayerStrafeController
{
    private readonly PlayerMover _playerMover;
    private readonly Transform _playerTransform;
    private readonly float _strafeSpeed;

    private const float LINE_OFFSET = 1;

    public int LeftLineNumber { get; private set; }
    public int RightLineNumber { get; private set; }
    public int CurrentLineNumber { get; private set; }
    public bool IsStrafing { get; private set; }

    public PlayerStrafeController(Transform playerTransform, PlayerMover playerMover, float strafeSpeed)
    {
        _playerMover = playerMover;
        _playerTransform = playerTransform;
        _strafeSpeed = strafeSpeed;
        LeftLineNumber = 1;
        RightLineNumber = 3;
        var midLineNumber = (LeftLineNumber + RightLineNumber) / 2;
        CurrentLineNumber = (int)playerTransform.position.x + midLineNumber;

    }

    public void StrafeRight(CancellationToken token)
    {
        var strafeDirection = Vector3.right;
        Strafe(strafeDirection, token);
    }

    public void StrafeLeft(CancellationToken token)
    {
        var strafeDirection = Vector3.left;
        Strafe(strafeDirection, token);
    }

    private void Strafe(Vector3 strafeDirection, CancellationToken token)
    {
        if (IsStrafing)
            throw new InvalidOperationException("Player already strafing");
        var newLine = CurrentLineNumber + (int)strafeDirection.x;
        if (newLine < LeftLineNumber || newLine > RightLineNumber)
            throw new ArgumentOutOfRangeException("Player out of lines");
        var targetLine = _playerTransform.position + LINE_OFFSET * strafeDirection;
        CurrentLineNumber = newLine;
        StrafeAsync(targetLine, token).Forget();
    }

    private async UniTaskVoid StrafeAsync(Vector3 targetLine, CancellationToken token)
    {
        IsStrafing = true;
        try
        {
            while (Mathf.Abs(_playerTransform.position.x - targetLine.x) > 1e-11)
            {
                if (GameManager.Instance.IsPaused)
                    await UniTask.WaitUntil(() => !GameManager.Instance.IsPaused);
                var newPosition = Vector3.MoveTowards(_playerTransform.position, targetLine, _strafeSpeed * Time.deltaTime);
                var newVelocity = _playerMover.Velocity;
                newVelocity.x = (newPosition - _playerTransform.position).x / Time.deltaTime;
                _playerMover.Velocity = newVelocity;
                await UniTask.NextFrame(token);
            }
        }
        finally
        {
            IsStrafing = false;
            var velocity = _playerMover.Velocity;
            velocity.x = 0;
            _playerMover.Velocity = velocity;
        }
    }
}