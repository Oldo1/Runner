using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRotator : MonoBehaviour, IService
{
    [SerializeField] private float _rotateSpeed;
    private HashSet<Transform> _coins;

    public void Init()
    {
        _coins = new HashSet<Transform>();
        GameEvents.OnSpawnSegment += (segment) =>
        {
            if (segment.TryGetSegmentCoins(out var coins))
            {
                foreach (var coin in coins)
                    _coins.Add(coin.transform);
            }   
        };

        GameEvents.OnDestroySegment += (segment) =>
        {
            if (segment.TryGetSegmentCoins(out var coins))
            {
                foreach (var coin in coins)
                    _coins.Remove(coin.transform);
            }
        };
        ServiceLocator.Register(this);
    }

    private void Update()
    {
        foreach (var coin in _coins)
            coin.Rotate(_rotateSpeed * Time.deltaTime % 360 * Vector3.up);
    }
}
