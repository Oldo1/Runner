using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class PlayerStrafe
{
    private readonly PlayerMover _playerMover;
    private readonly Transform _playerTransform;
    private readonly float _strafeSpeed;
    private readonly CancellationToken _cancellationToken;

    private const float LINE_OFFSET = 1;

    public int LeftLineNumber { get; private set; }
    public int RightLineNumber { get; private set; }
    public int CurrentLineNumber { get; private set; } 
    public bool IsStrafing { get; private set; }

    public PlayerStrafe(Transform playerTransform, PlayerMover playerMover, float strafeSpeed)
    {
        _cancellationToken.Register(() => EndStrafe());
        _playerMover = playerMover;
        _playerTransform = playerTransform;
        _strafeSpeed = strafeSpeed;
        LeftLineNumber = 1;
        RightLineNumber = 3;
        CurrentLineNumber = (LeftLineNumber + RightLineNumber) / 2;

    }

    public void StrafeRight(CancellationToken token)
    {
        if (IsStrafing)
            throw new InvalidOperationException();
        var strafeDirection = Vector3.right;
        CurrentLineNumber++;
        Strafe(strafeDirection, token);
    }

    public void StrafeLeft(CancellationToken token)
    {
        if (IsStrafing)
            throw new InvalidOperationException();
        var strafeDirection = Vector3.left;
        CurrentLineNumber--;
        Strafe(strafeDirection, token);
    }

    private void Strafe(Vector3 strafeDirection, CancellationToken token)
    {
        var targetLine = _playerTransform.position + LINE_OFFSET * strafeDirection;
        StrafeAsync(targetLine, token).Forget();
    }

    private async UniTaskVoid StrafeAsync(Vector3 targetLine, CancellationToken token)
    {
        token.Register(EndStrafe);
        IsStrafing = true;
        while (Mathf.Abs(_playerTransform.position.x - targetLine.x) > 1e-11)
        {
            var newPosition = Vector3.MoveTowards(_playerTransform.position, targetLine, _strafeSpeed * Time.deltaTime);
            var newVelocity = _playerMover.Velocity;
            newVelocity.x = (newPosition - _playerTransform.position).x / Time.deltaTime;
            _playerMover.Velocity = newVelocity;
            await UniTask.NextFrame(token);
        }
        EndStrafe();
    }

    private void EndStrafe()
    {
        IsStrafing = false;
        var velocity = _playerMover.Velocity;
        velocity.x = 0;
        _playerMover.Velocity = velocity;
    }
}