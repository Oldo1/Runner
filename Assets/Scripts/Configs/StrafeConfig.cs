using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Strafe config", menuName = "Strafe config")]
    public class StrafeConfig : ScriptableObject
    {
        [SerializeField] private int _maxStrafeForceX;
        [SerializeField] private int _maxStrafeForceY;

        public int MaxStrafeForceX => _maxStrafeForceX;
        public int MaxStrafeForceY => _maxStrafeForceY;
    }
}
