using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Value config", menuName = "Config files")]
    public class ValueConfig : ScriptableObject
    {
        [SerializeField] private int _value;

        public int Value => _value;
    }
}
