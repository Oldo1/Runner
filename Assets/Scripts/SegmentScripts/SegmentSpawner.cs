using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class SegmentSpawner
    {
        private readonly GameObject[] _segmentsPrefab;
        private readonly float _zOffset;
        private readonly int _maxSegmentsCount;
        private static Transform _lastCreatedSegmentTransform;

        public SegmentSpawner(GameObject[] segmentPrefab, float zOffset)
        {
            _segmentsPrefab = segmentPrefab;
            _zOffset = zOffset;
        }

        public static void FindLastCreatedSegmentTransform()
        {
            var segments = GameObject.FindObjectsByType<Segment>(FindObjectsSortMode.None);
            var lastCreatedSegmemt = segments.OrderBy(x => Vector3.Distance(x.transform.position, Camera.main.transform.position)).Last();
            _lastCreatedSegmentTransform = lastCreatedSegmemt.transform;
        }

        public Segment Spawn()
        {
            var segmentPrefab = _segmentsPrefab[UnityEngine.Random.Range(0, _segmentsPrefab.Length)];
            var newSpawnPosition = _lastCreatedSegmentTransform.position + Vector3.forward * _zOffset;
            var segmentGameObject = GameObjectPoolService.Spawn(segmentPrefab, newSpawnPosition);
            var segment = segmentGameObject.GetComponent<Segment>();
            _lastCreatedSegmentTransform = segment.transform;
            GameEvents.InvokeOnSpawnSegment(segment);
            return segment;
        }
    }
}
