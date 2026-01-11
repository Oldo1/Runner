using Assets.Scripts.States;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _strafeSpeed;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private JumpData _jumpData;
        [SerializeField] private CharacterController _characterController;

        private PlayerStrafe _playerStrafe;
        private StateMachine _stateMachine;
        private CancellationTokenSource _cancelationToken; 
        private PlayerGravityHandler _gravityHandler;

        public bool IsStrafing => _playerStrafe.IsStrafing;
        public bool IsOnGround => _characterController.isGrounded;
        public int LeftLineNumber => _playerStrafe.LeftLineNumber;
        public int RightLineNumber => _playerStrafe.RightLineNumber;
        public int CurrentLineNumber => _playerStrafe.CurrentLineNumber;

        public void Die()
        {
            _playerMover.enabled = false;
            _cancelationToken.Cancel();
            GameEvents.InvokeOnDieEvent();
        }

        public void Init()
        {
            _playerStrafe = new PlayerStrafe(transform, _playerMover, _strafeSpeed);
            _gravityHandler = new PlayerGravityHandler(_playerMover, _jumpData.Gravity);
            _stateMachine = new StateMachine(this);
        }

        public void StrafeRight()
        {
            _playerStrafe.StrafeRight(_cancelationToken.Token);
        }

        public void StrafeLeft()
        {
            _playerStrafe.StrafeLeft(_cancelationToken.Token);
        }

        public void Jump()
        {
            _playerMover.Velocity = Vector3.up * _jumpData.InitialVelocity;
        }

        public void Fall()
        {
            _gravityHandler.HandleGravity(_cancelationToken.Token).Forget();
        }

        public void Collect(GameObject coin)
        {
            Destroy(coin);
            Debug.Log("Coin collect");
            GameEvents.InvokeOnCollectCoinEvent(coin);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnEnable()
        {
            _cancelationToken?.Dispose();
            _cancelationToken = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancelationToken.Cancel();
        }

        private void OnDestroy()
        {
            _cancelationToken.Cancel();
        }
    }
}
