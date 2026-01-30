using Assets;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private GameObject[] _obstaclesPrefabs;
    [SerializeField] private TextMeshProUGUI _coinsNumber;
    [SerializeField] private GameManager _game;
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

    private void Awake()
    {
        SegmentSpawner.FindLastCreatedSegmentTransform();
        _coinsRotator.Init();
        var segmentSpawnerAsync = new SegmentsSpawnerAsync(_obstaclesPrefabs, _zOffset, _obstaclesSpawnRate);
        var segmentSpawner = new SegmentsSpawner(_obstaclesPrefabs, _zOffset);
        segmentSpawner.Spawn(10);
        var playerSpawner = new PlayerSpawner(_playerPrefab);
        SubscribeEvents();
        var player = playerSpawner.Spawn(_playerSpawnPosition.position);
        player.Init();
        _segmentMover.Init();
        _cameraFollow.Init(player.transform);
        _game.Init(_gameOverText, _restartHint, _gameStartHintUI.transform);
        _segmentMover.enabled = false;
    }



    private void SubscribeEvents()
    {
        GameEvents.OnChangeCoinNumber += UpdateCoinNumber;
        GameEvents.OnGameOver += SetActiveGameOverUI;
        GameEvents.OnStartGame += SetInactivePanelUI;
        GameEvents.OnStartGame += SetInActiveStartHintUI;
        GameEvents.OnGameOver += SetActivePanelUi;
        GameEvents.OnSpawnSegment += RespawnCoins;
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

    private void SetActivePanelUi()
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
        GameEvents.OnChangeCoinNumber -= UpdateCoinNumber;
        GameEvents.OnGameOver -= SetActiveGameOverUI;
        GameEvents.OnStartGame -= SetInactivePanelUI;
        GameEvents.OnStartGame -= SetInActiveStartHintUI;
        GameEvents.OnGameOver -= SetActivePanelUi;
        GameEvents.OnSpawnSegment -= RespawnCoins;
        DisposeAll();
        DisposablesContainer.Clear();
        GameObjectPoolService.Clear();
        ServiceLocator.Clear();
    }

    private void DisposeAll()
    {
        var disposables = DisposablesContainer.Disposables;
        foreach (var disposable in disposables)
            disposable.Dispose();
    }
}
