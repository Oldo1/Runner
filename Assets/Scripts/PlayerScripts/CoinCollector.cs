using UnityEngine;

namespace Assets.Scripts
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] Player _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
                _player.Collect(other.gameObject);
        }
    }
}
