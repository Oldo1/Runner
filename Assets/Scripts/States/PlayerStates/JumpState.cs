using Assets.Scripts.PlayerScripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class JumpState : GeneralState
    {
        private readonly Player _player;
        private readonly PlayerAnimationController _playerAnimationController;

        public JumpState(Player player, PlayerStateMachine stateMachine, IInputHandler inputHandler,
            PlayerAnimationController playerAnimationController) : base(player, stateMachine, inputHandler)
        {
            _player = player;
            _playerAnimationController = playerAnimationController;
        }

        public override void OnEnter()
        {
            Debug.Log("Jump state");
            _playerAnimationController.SetIsJumpingParameter(true);
            JumpAsync().Forget();
            base.OnEnter();
        }

        private async UniTaskVoid JumpAsync()
        {
            _player.Jump();
            await UniTask.WaitUntil(() => !_player.IsOnGround);
            _player.Fall();
        }

        public override void OnExit()
        {
            _playerAnimationController.SetIsJumpingParameter(false);
            base.OnExit();
        }
    }
}
