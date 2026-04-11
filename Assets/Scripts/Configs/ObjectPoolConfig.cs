using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Object pool config", menuName = "Object pool config")]
    public class ObjectPoolConfig : ScriptableObject
    {
        [SerializeField] private int _initialCapacity;
        [SerializeField] private int _maxCapacity;

        public int InitialCapacity => _initialCapacity;
        public int MaxCapacity => _maxCapacity;
    }
}
