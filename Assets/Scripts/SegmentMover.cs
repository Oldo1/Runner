using Assets.Scripts;
using Assets.Scripts.States;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private HashSet<GameObject> _segments;

    public void Init()
    {
        _segments = FindObjectsByType<Segment>(FindObjectsSortMode.None).Select(x => x.gameObject).ToHashSet();
        GameEvents.OnSpawnSegment += AddSegment;
        GameEvents.OnDestroySegment += RemoveSegment;
        GameEvents.OnGameOver += DisableObject;
        GameEvents.OnStartGame += () => enabled = true;
    }

    private void AddSegment(GameObject segment)
    {
        _segments.Add(segment);
    }

    private void RemoveSegment(GameObject segment)
    {
        _segments.Remove(segment);
    }

    private void DisableObject()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= DisableObject;
        GameEvents.OnDestroySegment -= RemoveSegment;
        GameEvents.OnGameOver -= DisableObject;
    }

    private void OnApplicationQuit()
    {
        GameEvents.OnGameOver -= DisableObject;
        GameEvents.OnDestroySegment -= RemoveSegment;
        GameEvents.OnGameOver -= DisableObject;
    }

    private void LateUpdate()
    {
        foreach (var segment in _segments)
        {
            if (segment != null)
                segment.transform.Translate(_moveSpeed * Time.deltaTime * -transform.forward);
        }
    }
}
