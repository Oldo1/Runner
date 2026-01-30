using Assets.Scripts.PlayerScripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class JumpState : GeneralState
    {
        private readonly Player _player;

        public JumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Jump state");
            PlayerAnimationController.Instance.SetIsJumpingParameter(true);
            JumpAsync().Forget();
        }

        private async UniTaskVoid JumpAsync()
        {
            _player.Jump();
            await UniTask.WaitUntil(() => !_player.IsOnGround);
            _player.Fall();
        }

        public override void OnExit()
        {
            PlayerAnimationController.Instance.SetIsJumpingParameter(false);
        }
    }
}
