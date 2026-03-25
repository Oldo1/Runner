using System;

namespace Assets.Scripts.PlayerScripts
{
    public class UIInputHandler: IDisposable
    {
        private readonly PlayerInput _playerInput;

        public UIInputHandler(PlayerInput input)
        {
            if (input == null)
                throw new ArgumentNullException("input is null");
            _playerInput = input;
            GameEvents.OnPause += OnPause;
        }

        public void OnPause() 
        {
            Enable();
        }

        public void Enable()
        {
            _playerInput.UI.Enable();
        }

        public bool WasTap()
        {
            return _playerInput.UI.Tap.WasPressedThisFrame();
        }

        public void Disable()
        {
            _playerInput.UI.Disable();
        }

        public void Dispose()
        {
            GameEvents.OnPause -= OnPause;
            _playerInput.UI.Disable();
        }
    }
}
