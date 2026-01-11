using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform _playerTransform;
    private Vector3 _offset;

    public void Init(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _offset = transform.position - _playerTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position + _offset;
    }
}
