using System;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class GameplayInputHandler: IDisposable, IInputHandler, IDisable
    {

        private bool _strafePerformed;
        private bool _jumpPerformed;
        private readonly PlayerInput _playerInput;
        public event Action<Vector2> OnStrafePerformed;
        public event Action OnJumpPerformed;

        public GameplayInputHandler(PlayerInput input)
        {
            if (input == null)
                throw new ArgumentNullException("input is null");
            _playerInput = input;
            Enable();
            _playerInput.Gameplay.Swipe.performed += SwipePerformed;
            _playerInput.Gameplay.Touch.started += OnTouchCanceled;
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
                if (Mathf.Abs(strafeValue.x) >= 60)
                {
                    _strafePerformed = true;
                    var strafeDirection = Vector2.right * Mathf.Sign(strafeValue.x);
                    OnStrafePerformed?.Invoke(strafeDirection);
                }
            }

            if (!_jumpPerformed)
            {
                var swipeDirection = context.ReadValue<Vector2>();
                if (swipeDirection.y >= 60)
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
