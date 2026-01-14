using Assets.Scripts.States;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private HashSet<GameObject> _segments;

        public void Init()
        {
            _segments = FindObjectsByType<Segment>(FindObjectsSortMode.None).Select(x => x.gameObject).ToHashSet();
            GameEvents.OnSpawnSegment += AddSegment;
            GameEvents.OnDestroySegment += RemoveSegment;
            GameEvents.OnGameOver += DisableObject;
            GameEvents.OnStartGame += EnableObject;
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

        private void EnableObject()
        {
            enabled = true;
        }

        private void UnSubscribeEvents()
        {
            GameEvents.OnSpawnSegment -= AddSegment;
            GameEvents.OnDestroySegment -= RemoveSegment;
            GameEvents.OnGameOver -= DisableObject;
            GameEvents.OnStartGame -= EnableObject;
        }

        private void OnApplicationQuit()
        {
            UnSubscribeEvents();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            foreach (var segment in _segments)
            {
                if (segment != null)
                    segment.transform.Translate(_moveSpeed * Time.deltaTime * -transform.forward);
            }
        }
    }
}

