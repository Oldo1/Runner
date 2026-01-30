using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class PlayerAnimationController : IService
    {
        private readonly Animator _animator;
        public static PlayerAnimationController Instance { get; private set; }

        public PlayerAnimationController(Animator animator)
        {
            if (Instance == null || Instance != this)
                Instance = this;
            _animator = animator;
            _animator.speed = 0;
            ServiceLocator.Register(this);
        }

        public void Play()
        {
            _animator.speed = 1;
        }

        public void Pause() 
        {
            _animator.speed = 0;
        }

        public void SetIsJumpingParameter(bool value)
        {
            _animator.SetBool("IsJumping", value);
        }
    }
}
