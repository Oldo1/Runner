using Assets.Scripts;
using UnityEngine;

public class SegmentDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DestroyTrigger component))
        {
            Destroy(gameObject);
            GameEvents.InvokeOnDestroySegment(gameObject);
        }
    }
}
