using Assets;
using Assets.Scripts;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject[] _segmentsPrefabs;

    [Header("UI")]
    [SerializeField] private GameObject _panelUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gameStartHintUI;
    [SerializeField] private Transform _gameOverText;
    [SerializeField] private Transform _restartHint;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private CoinsRotator _coinsRotator;
    [SerializeField] private Transform _pauseText;
    [SerializeField] private TextMeshProUGUI _coinsNumber;

    [Header("Configs")]
    [SerializeField] private JumpConfig _jumpConfig;
    [SerializeField] private StrafeConfig _strafeConfig;
    [SerializeField] private ValueConfig _strafeSpeed;
    [SerializeField] private ValueConfig _initialSegmentCount;
    [SerializeField] private SegmentSpawnerConfig _segmentSpawnerConfig;
    [SerializeField] private ObjectPoolConfig _objectPoolConfig;

    [Header("Other")]
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SegmentsMover _segmentMover;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Transform _playerSpawnPosition;


    private List<IDisposable> _disposables;
    private ScaleLoopAnimation _pauseAnimation;

    private void Awake()
    {
        _disposables = new List<IDisposable>();
        SegmentSpawner.FindLastCreatedSegmentTransform();
        _coinsRotator.Init();

        _soundManager.Init();

        var segmentSpawnerAsync = new SegmentsSpawnerAsync(_segmentsPrefabs, _segmentSpawnerConfig.ZOffset, _segmentSpawnerConfig.SegmentSpawnRate, _gameManager);
        var segmentSpawner = new SegmentsSpawner(_segmentsPrefabs, _segmentSpawnerConfig.ZOffset);
        AddGameObjectPools(_segmentsPrefabs);
        segmentSpawner.Spawn(_initialSegmentCount.Value);

        var playerSpawner = new PlayerSpawner(_playerPrefab);
        var playerInput = new PlayerInput();
        var gameplayInputHandler = new GameplayInputHandler(playerInput, _strafeConfig);
        var UIInputHandler = new UIInputHandler(playerInput);

        SubscribeEvents();
        var player = playerSpawner.Spawn(_playerSpawnPosition.position);
        var playerMover = player.GetComponent<PlayerMover>();
        var playerStateMachine = player.GetComponent<PlayerStateMachine>();

        var strafeController = new PlayerStrafeController(player.transform, playerMover, _strafeSpeed.Value, _gameManager);
        var playerGravityHandler = new PlayerGravityHandler(playerMover, _jumpConfig.Gravity, _gameManager);
        var playerAnimationController = new PlayerAnimationController(player.GetComponentInChildren<Animator>());
        var gameStartHintAnimation = new ScaleLoopAnimation(_gameStartHintUI.transform);
        var pauseAnimation = new ScaleLoopAnimation(_pauseText);
        var gameOverAnimation = new GameOverAnimation(_gameOverText, _restartHint);

        _pauseAnimation = pauseAnimation;

        player.Init(strafeController, playerGravityHandler, _jumpConfig);
        playerStateMachine.Init(player, gameplayInputHandler, gameplayInputHandler, playerAnimationController);
        _segmentMover.Init();
        _cameraFollow.Init(player.transform);
        _gameManager.Init(gameOverAnimation, gameStartHintAnimation, UIInputHandler, gameplayInputHandler, playerMover, playerStateMachine, segmentSpawnerAsync, _coinsRotator,
            playerAnimationController, _segmentMover);

        AddDisposables(gameplayInputHandler, UIInputHandler, gameOverAnimation,
            playerInput, gameStartHintAnimation, _coinsRotator, _segmentMover, playerAnimationController);
    }

    private void OnPause()
    {
        _pauseUI.SetActive(true);
        _pauseAnimation.Play();
    }

    private void OnResume()
    {
        _pauseUI.SetActive(false);
        _pauseAnimation.Kill();
    }

    private void AddGameObjectPools(params GameObject[] prefabs)
    {
        foreach (var prefab in prefabs)
        {
            var objectPool = new GameObjectPool(prefab, _objectPoolConfig.InitialCapacity, _objectPoolConfig.MaxCapacity);
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
        GameEvents.OnPause += OnPause;
        GameEvents.OnResume += OnResume;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.OnChangeCoinNumber -= UpdateCoinNumber;
        GameEvents.OnGameOver -= SetActiveGameOverUI;
        GameEvents.OnStartGame -= SetInactivePanelUI;
        GameEvents.OnStartGame -= SetInActiveStartHintUI;
        GameEvents.OnGameOver -= SetActivePanelUI;
        GameEvents.OnSpawnSegment -= RespawnCoins;
        GameEvents.OnPause -= SetActivePanelUI;
        GameEvents.OnResume -= SetInactivePanelUI;
        GameEvents.OnPause -= OnPause;
        GameEvents.OnResume -= OnResume;
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