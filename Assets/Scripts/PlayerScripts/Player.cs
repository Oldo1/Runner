using Assets.Scripts.Configs;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _strafeSpeed;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private JumpConfig _jumpConfig;

        private PlayerStrafeController _strafeController;
        private CancellationTokenSource _cancelationToken; 
        private PlayerGravityHandler _gravityHandler;

        public bool IsStrafing => _strafeController.IsStrafing;
        public bool IsOnGround => _characterController.isGrounded;
        public bool IsGravityHandling => _gravityHandler.IsGravityHandling;
        public int LeftLineNumber => _strafeController.LeftLineNumber;
        public int RightLineNumber => _strafeController.RightLineNumber;
        public int CurrentLineNumber => _strafeController.CurrentLineNumber;

        public void Init(PlayerStrafeController strafeController, PlayerGravityHandler gravityHandler, JumpConfig jumpConfig)
        {
            _strafeController = strafeController;
            _gravityHandler = gravityHandler;
            _jumpConfig = jumpConfig;
        }

        public void Die()
        {
            _playerMover.enabled = false;
            _cancelationToken.Cancel();
            GameEvents.InvokeOnDieEvent();
        }

        public void StrafeRight()
        {
            _strafeController.StrafeRight(_cancelationToken.Token);
        }

        public void StrafeLeft()
        {
            _strafeController.StrafeLeft(_cancelationToken.Token);
        }

        public void Jump()
        {
            _playerMover.Velocity = Vector3.up * _jumpConfig.InitialVelocity;
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