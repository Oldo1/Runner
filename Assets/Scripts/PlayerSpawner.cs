using Assets.Scripts;
using UnityEngine;

public class PlayerSpawner
{
    private readonly Player _playerPrefab;

    public PlayerSpawner(Player playerPrefab)
    {
        _playerPrefab = playerPrefab;
    }

    public Player Spawn(Vector3 spawnPosition)
    {
        return GameObject.Instantiate(_playerPrefab, spawnPosition, Quaternion.identity);
    }
}
