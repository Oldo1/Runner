using Assets.Scripts;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnDestroy()
    {
        Instantiate(_particleSystem, transform.position, Quaternion.identity);
    }
}
