using Assets.Scripts;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private GameObject[] _obstaclesPrefabs;
    [SerializeField] private GameObject _lastCreatedSegment;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _coinsNumber;
    [SerializeField] private Transform _coinsTextTransform;
    [SerializeField] private Game _game;
    [SerializeField] private SegmentMover _segmentMover;
    [SerializeField] private float _obstaclesSpawnRate;
    [SerializeField] private float _zOffset;
    
    private void Awake()
    {
        var playerSpawner = new PlayerSpawner(_playerPrefab);
        var obstacleSpawner = new SegmentsSpawner(_obstaclesPrefabs, _lastCreatedSegment, _zOffset, _obstaclesSpawnRate);
        GameEvents.OnChangeCoinNumber += (newCoinsNumber) => _coinsNumber.text = newCoinsNumber.ToString();
        GameEvents.OnGameOver += () => _gameOverUI.SetActive(true);
        var player = playerSpawner.Spawn(_playerSpawnPosition.position);
        _segmentMover.Init();
        player.Init();
        _cameraFollow.Init(player.transform);
        _game.Init();
        var playerMover = player.GetComponent<PlayerMover>();
        _segmentMover.enabled = false;
        playerMover.enabled = false;
    }
}
