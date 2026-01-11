using Assets.Scripts;
using UnityEngine;

public class PlayerDieController : MonoBehaviour
{
    [SerializeField] Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _player.Die();
        }
    }
}
 