using DG.Tweening;
using System;
using UnityEngine;

public class GameOverAnimation : IDisposable
{
    private readonly Transform _gameOverText;
    private readonly Transform _restartHint;

    public GameOverAnimation(Transform gameOverText, Transform restartHint)
    {
        _gameOverText = gameOverText;
        _restartHint = restartHint;
    }

    public void Play()
    {
        _gameOverText.DOMove(_gameOverText.position, 2f).SetEase(Ease.OutElastic).From(_gameOverText.position + 50 * Vector3.up);
        _restartHint.DOScale(new Vector3(1.15f, 1.15f, 0f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Kill()
    {
        _gameOverText.DOKill();
        _restartHint.DOKill();
    }

    public void Dispose()
    {
        Kill();
    }
}
