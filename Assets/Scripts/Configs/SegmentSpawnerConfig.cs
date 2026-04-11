using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Segment spawner config", menuName = "Segment spawner config")]
    public class SegmentSpawnerConfig : ScriptableObject
    {
        [SerializeField] private float _segmentSpawnRate;
        [SerializeField] private float _zOffset;

        public float SegmentSpawnRate => _segmentSpawnRate;
        public float ZOffset => _zOffset;
    }
}
