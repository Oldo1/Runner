using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentsMover : MonoBehaviour, IDisposable
    {
        [SerializeField] private float _moveSpeed;
        private HashSet<Segment> _segments;

        public void Init()
        {
            _segments = FindObjectsByType<Segment>(FindObjectsSortMode.None).ToHashSet();
            GameEvents.OnSpawnSegment += AddSegment;
            GameEvents.OnDestroySegment += RemoveSegment;
            GameEvents.OnPause += Disable;
            GameEvents.OnResume += Enable;
            GameEvents.OnStartGame += Enable;
        }


        public void Dispose()
        {
            GameEvents.OnSpawnSegment -= AddSegment;
            GameEvents.OnDestroySegment -= RemoveSegment;
            GameEvents.OnPause -= Disable;
            GameEvents.OnResume -= Enable;
            GameEvents.OnStartGame -= Enable;
        }

        private void Enable()
        {
            enabled = true;
        }

        private void AddSegment(Segment segment)
        {
            _segments.Add(segment);
        }

        private void RemoveSegment(Segment segment)
        {
            _segments.Remove(segment);
        }

        private void Disable()
        {
            enabled = false;
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

