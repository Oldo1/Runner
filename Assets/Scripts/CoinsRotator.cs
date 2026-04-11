using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRotator : MonoBehaviour, IDisposable
{
    [SerializeField] private float _rotateSpeed;
    private HashSet<Transform> _coins;

    public void Init()
    {
        _coins = new HashSet<Transform>();
        GameEvents.OnSpawnSegment += OnSpawnSegment;
        GameEvents.OnDestroySegment += OnDestroySegment;
        GameEvents.OnPause += Disable;
        GameEvents.OnResume += Enable;
        GameEvents.OnStartGame += Enable;
    }

    private void OnSpawnSegment(Assets.Scripts.Segment segment)
    {
        if (segment.TryGetSegmentCoins(out var coins))
        {
            foreach (var coin in coins)
                _coins.Add(coin.transform);
        }
    }

    private void OnDestroySegment(Assets.Scripts.Segment segment)
    {
        if (segment.TryGetSegmentCoins(out var coins))
        {
            foreach (var coin in coins)
                _coins.Remove(coin.transform);
        }
    }

    private void Disable()
    {
        enabled = false;
    }

    private void Enable()
    {
        enabled = true;
    }

    private void Update()
    {
        foreach (var coin in _coins)
            coin.Rotate(_rotateSpeed * Time.deltaTime % 360 * Vector3.up);
    }

    public void Dispose()
    {
        GameEvents.OnSpawnSegment -= OnSpawnSegment;
        GameEvents.OnDestroySegment -= OnDestroySegment;
        GameEvents.OnPause -= Disable;
        GameEvents.OnResume -= Enable;
        GameEvents.OnStartGame -= Enable;
    }
}