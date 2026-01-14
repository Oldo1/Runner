using Assets.Scripts;
using Assets.Scripts.States;
using UnityEngine;

public class SegmentsDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var parentGameObject = other.transform.parent.gameObject;
        if (parentGameObject != null && parentGameObject.TryGetComponent(out Segment segment))
        {
            Destroy(parentGameObject);
            GameEvents.InvokeOnDestroySegment(parentGameObject);
        }
    }
}
