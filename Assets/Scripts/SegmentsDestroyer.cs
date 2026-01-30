using Assets.Scripts;
using UnityEngine;

public class SegmentsDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var parentGameObject = other.transform.parent.gameObject;
        if (parentGameObject != null && parentGameObject.TryGetComponent(out Segment segment))
        {
            if (!GameObjectPoolService.IsSpawned(parentGameObject))
                Destroy(parentGameObject);
            else
                GameObjectPoolService.Despawn(parentGameObject);
            GameEvents.InvokeOnDestroySegment(segment);
        }
    }
}