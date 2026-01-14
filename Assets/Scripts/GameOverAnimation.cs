using DG.Tweening;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
    [SerializeField] private Transform _gameOverText;
    [SerializeField] private Transform _restartHint;

    private void Awake()
    {
        _gameOverText.DOMove(_gameOverText.position, 2f).SetEase(Ease.OutElastic).From(_gameOverText.position + 50 * Vector3.up);
        _restartHint.DOScale(new Vector3(1.15f, 1.15f, 0f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        _gameOverText.DOKill();
        _restartHint.DOKill();
    }
}
