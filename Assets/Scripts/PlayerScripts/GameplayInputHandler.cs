using Assets.Scripts.Configs;
using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class GameplayInputHandler: IDisposable, IInputHandler, IDisable
    {

        private bool _strafePerformed;
        private bool _jumpPerformed;
        private readonly PlayerInput _playerInput;
        private readonly StrafeConfig _strafeConfig;
        public event Action<Vector2> OnStrafePerformed;
        public event Action OnJumpPerformed;

        public GameplayInputHandler(PlayerInput input, StrafeConfig strafeConfig)
        {
            if (input == null)
                throw new ArgumentNullException("input is null");
            _playerInput = input;
            _strafeConfig = strafeConfig;
            Enable();
            _playerInput.Gameplay.Swipe.performed += SwipePerformed;
            _playerInput.Gameplay.Touch.started += OnTouchCanceled;
            GameEvents.OnPause += Disable;
            GameEvents.OnResume += Enable;
        }

        private void OnTouchCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("touch canceled");
            _strafePerformed = false;
            _jumpPerformed = false;
        }

        private void SwipePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (!_strafePerformed)
            {
                var strafeValue = context.ReadValue<Vector2>();
                if (Mathf.Abs(strafeValue.x) >= _strafeConfig.MaxStrafeForceX)
                {
                    _strafePerformed = true;
                    var strafeDirection = Vector2.right * Mathf.Sign(strafeValue.x);
                    OnStrafePerformed?.Invoke(strafeDirection);
                }
            }

            if (!_jumpPerformed)
            {
                var swipeDirection = context.ReadValue<Vector2>();
                if (swipeDirection.y >= _strafeConfig.MaxStrafeForceY)
                {
                    _jumpPerformed = true;
                    OnJumpPerformed?.Invoke();
                }
            }
        }

        public void Dispose()
        {
            _playerInput.Gameplay.Swipe.performed -= SwipePerformed;
            _playerInput.Gameplay.Touch.started -= OnTouchCanceled;
            GameEvents.OnPause -= Disable;
            GameEvents.OnResume -= Enable;
            _playerInput.Gameplay.Disable();
        }

        public void Disable()
        {
            _playerInput.Gameplay.Disable();
        }

        public void Enable()
        {
            _playerInput.Gameplay.Enable();
        }
    } 
}
