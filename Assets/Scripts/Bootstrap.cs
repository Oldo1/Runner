using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private GameObject[] _obstaclesPrefabs;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _coinsNumber;
    [SerializeField] private Transform _coinsTextTransform;
    [SerializeField] private GameManager _game;
    [SerializeField] private SegmentsMover _segmentMover;
    [SerializeField] private float _obstaclesSpawnRate;
    [SerializeField] private float _zOffset;

    private IEnumerable<IDisposable> _disposables;

    private void Awake()
    {
        var segmentSpawner = new SegmentsSpawner(_obstaclesPrefabs, _zOffset, _obstaclesSpawnRate);
        var playerSpawner = new PlayerSpawner(_playerPrefab);
        GameEvents.OnChangeCoinNumber += UpdateCoinNumber;
        GameEvents.OnGameOver += SetActiveGameOverUI;
        var player = playerSpawner.Spawn(_playerSpawnPosition.position);
        _segmentMover.Init();
        player.Init();
        _cameraFollow.Init(player.transform);
        _game.Init();
        var playerMover = player.GetComponent<PlayerMover>();
        _segmentMover.enabled = false;
        playerMover.enabled = false;
        AddDisposables(segmentSpawner);
    }

    private void AddDisposables(params IDisposable[] disposables)
    {
        _disposables = disposables;
    }

    private void SetActiveGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }

    private void UpdateCoinNumber(int newCoinsNumber)
    {
        _coinsNumber.text = newCoinsNumber.ToString();
    }

    private void OnDestroy()
    {
        GameEvents.OnChangeCoinNumber -= UpdateCoinNumber;
        GameEvents.OnGameOver -= SetActiveGameOverUI;
        foreach (var disposable in _disposables)
            disposable.Dispose();
    }
}
