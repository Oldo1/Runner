using Assets.Scripts;
using System;
using UnityEngine;

namespace Assets
{
    public class PlayerAnimationController : IDisposable
    {
        private readonly Animator _animator;

        public PlayerAnimationController(Animator animator)
        {
            _animator = animator;
            _animator.speed = 0;
            GameEvents.OnPause += Pause;
            GameEvents.OnResume += Play;
            GameEvents.OnStartGame += Play;
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

        public void Dispose()
        {
            GameEvents.OnPause -= Pause;
            GameEvents.OnResume -= Play;
            GameEvents.OnStartGame -= Play;
        }
    }
}
