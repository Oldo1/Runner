using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Jump data", menuName = "Data files")]
    public class JumpData : ScriptableObject
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;

        private float MaxHeightJumpTime => _jumpTime / 2;
        public float Gravity => 2 * _jumpHeight / Mathf.Pow(MaxHeightJumpTime, 2);
        public float InitialVelocity => 2 * _jumpHeight / MaxHeightJumpTime;
    }
}
