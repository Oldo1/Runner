using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Jump config", menuName = "Jump files")]
    public class JumpConfig : ScriptableObject
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;

        private float MaxHeightJumpTime => _jumpTime / 2;
        public float Gravity => 2 * _jumpHeight / Mathf.Pow(MaxHeightJumpTime, 2);
        public float InitialVelocity => 2 * _jumpHeight / MaxHeightJumpTime;
    }
}
