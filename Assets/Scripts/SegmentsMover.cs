using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsMover : MonoBehaviour, IService
    {
        [SerializeField] private float _moveSpeed;
        private HashSet<Segment> _segments;

        public void Init()
        {
            _segments = FindObjectsByType<Segment>(FindObjectsSortMode.None).ToHashSet();
            ServiceLocator.Register(this);
            GameEvents.OnSpawnSegment += AddSegment;
            GameEvents.OnDestroySegment += RemoveSegment;
        }

        private void AddSegment(Segment segment)
        {
            _segments.Add(segment);
        }

        private void RemoveSegment(Segment segment)
        {
            _segments.Remove(segment);
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

