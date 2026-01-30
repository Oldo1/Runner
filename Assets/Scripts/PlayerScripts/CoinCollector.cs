using UnityEngine;

namespace Assets.Scripts
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] Player _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Coin coin))
                _player.Collect(coin);
        }
    }
}
