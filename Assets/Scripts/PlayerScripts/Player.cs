using Assets.Scripts.PlayerScripts;
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
        [SerializeField] private PlayerStateMachine _stateMachine;
        [SerializeField] private Animator _playerAnimator;

        private PlayerStrafeController _playerStrafe;
        private CancellationTokenSource _cancelationToken; 
        private PlayerGravityHandler _gravityHandler;

        public bool IsStrafing => _playerStrafe.IsStrafing;
        public bool IsOnGround => _characterController.isGrounded;
        public bool IsGravityHandling => _gravityHandler.IsGravityHandling;
        public int LeftLineNumber => _playerStrafe.LeftLineNumber;
        public int RightLineNumber => _playerStrafe.RightLineNumber;
        public int CurrentLineNumber => _playerStrafe.CurrentLineNumber;

        public void Init()
        {
            _playerMover.Init();
            _stateMachine.Init(this);
            _playerStrafe = new PlayerStrafeController(transform, _playerMover, _strafeSpeed);
            _gravityHandler = new PlayerGravityHandler(_playerMover, _jumpData.Gravity);
            var animationHandler = new PlayerAnimationController(_playerAnimator);
            _playerMover.enabled = false;
            _stateMachine.enabled = false;
        }

        public void Die()
        {
            _playerMover.enabled = false;
            _cancelationToken.Cancel();
            GameEvents.InvokeOnDieEvent();
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

        public void Collect(Coin coin)
        {
            coin.gameObject.SetActive(false);
            Debug.Log("Coin collected");
            GameEvents.InvokeOnCollectCoinEvent(coin);
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