using Assets;
using Assets.Scripts;
using Assets.Scripts.PlayerScripts;
using Cysharp.Threading.Tasks.Triggers;
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
    [SerializeField] private GameObject[] _segmentsPrefabs;
    [SerializeField] private TextMeshProUGUI _coinsNumber;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SegmentsMover _segmentMover;
    [SerializeField] private float _obstaclesSpawnRate;
    [SerializeField] private float _zOffset;
    [SerializeField] private GameObject _panelUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gameStartHintUI;
    [SerializeField] private Transform _gameOverText;
    [SerializeField] private Transform _restartHint;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private CoinsRotator _coinsRotator;
    [SerializeField] private GameObject _pauseText;
    [SerializeField] private JumpData _jumpData;

    private List<IDisposable> _disposables;

    private void Awake()
    {
        _disposables = new List<IDisposable>();
        SegmentSpawner.FindLastCreatedSegmentTransform();
        _coinsRotator.Init();

        var segmentSpawnerAsync = new SegmentsSpawnerAsync(_segmentsPrefabs, _zOffset, _obstaclesSpawnRate, _gameManager);
        var segmentSpawner = new SegmentsSpawner(_segmentsPrefabs, _zOffset);
        AddGameObjectPools(_segmentsPrefabs);
        segmentSpawner.Spawn(10);

        var playerSpawner = new PlayerSpawner(_playerPrefab);
        var playerInput = new PlayerInput();
        var gameplayInputHandler = new GameplayInputHandler(playerInput);
        var UIInputHandler = new UIInputHandler(playerInput);

        SubscribeEvents();
        var player = playerSpawner.Spawn(_playerSpawnPosition.position);
        var playerMover = player.GetComponent<PlayerMover>();
        var playerStateMachine = player.GetComponent<PlayerStateMachine>();

        var strafeStrafeController = new PlayerStrafeController(player.transform, playerMover, strafeSpeed: 10, _gameManager);
        var playerGravityHandler = new PlayerGravityHandler(playerMover, _jumpData.Gravity, _gameManager);
        var playerAnimationController = new PlayerAnimationController(player.GetComponentInChildren<Animator>());
        var scaleLoopAnimation = new ScaleLoopAnimation(_gameStartHintUI.transform);
        var gameOverAnimation = new GameOverAnimation(_gameOverText, _restartHint);

        player.Init(strafeStrafeController, playerGravityHandler);
        playerStateMachine.Init(player, gameplayInputHandler, gameplayInputHandler, playerAnimationController);
        _segmentMover.Init();
        _cameraFollow.Init(player.transform);
        _gameManager.Init(gameOverAnimation, scaleLoopAnimation, UIInputHandler, gameplayInputHandler, playerMover, playerStateMachine, segmentSpawnerAsync, _coinsRotator,
            playerAnimationController, _segmentMover);

        AddDisposables(gameplayInputHandler, UIInputHandler, gameOverAnimation, 
            playerInput, scaleLoopAnimation, _coinsRotator, _segmentMover, playerAnimationController);
    }

    private void AddGameObjectPools(params GameObject[] prefabs)
    {
        foreach (var prefab in prefabs)
        {
            var objectPool = new GameObjectPool(prefab, initialCapacity: 20, maxSize: 30);
            GameObjectPoolService.AddGameObjectPool(objectPool);
            _disposables.Add(objectPool);
        }

    }

    private void AddDisposables(params IDisposable[] disposables)
    {
        foreach (var disposable in disposables.Distinct())
            _disposables.Add(disposable);
    }

    private void SubscribeEvents()
    {
        GameEvents.OnChangeCoinNumber += UpdateCoinNumber;
        GameEvents.OnGameOver += SetActiveGameOverUI;
        GameEvents.OnStartGame += SetInactivePanelUI;
        GameEvents.OnStartGame += SetInActiveStartHintUI;
        GameEvents.OnGameOver += SetActivePanelUI;
        GameEvents.OnSpawnSegment += RespawnCoins;
        GameEvents.OnPause += SetActivePanelUI;
        GameEvents.OnResume += SetInactivePanelUI;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.OnChangeCoinNumber += UpdateCoinNumber;
        GameEvents.OnGameOver += SetActiveGameOverUI;
        GameEvents.OnStartGame += SetInactivePanelUI;
        GameEvents.OnStartGame += SetInActiveStartHintUI;
        GameEvents.OnGameOver += SetActivePanelUI;
        GameEvents.OnSpawnSegment += RespawnCoins;
        GameEvents.OnPause += SetActivePanelUI;
        GameEvents.OnResume += SetInactivePanelUI;
    }

    public void RespawnCoins(Segment segment)
    {
        if (segment.TryGetSegmentCoins(out var coins))
        {
            foreach (var coin in coins)
            {
                coin.SetActive(true);
            }
        }
    }

    private void SetActivePanelUI()
    {
        _panelUI.SetActive(true);
    }

    private void SetActiveGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }

    private void SetInactivePanelUI()
    {
        _panelUI.SetActive(false);
    }

    private void UpdateCoinNumber(int newCoinsNumber)
    {
        _coinsNumber.text = newCoinsNumber.ToString();
    }

    private void SetInActiveStartHintUI()
    {
        _gameStartHintUI.SetActive(false);
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
        DisposeAll();
        _disposables.Clear();
        GameObjectPoolService.Clear();
    }

    private void DisposeAll()
    {
        foreach (var disposable in _disposables)
            disposable.Dispose();
    }
}
