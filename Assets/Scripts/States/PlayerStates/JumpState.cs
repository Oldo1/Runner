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
            JumpAsync().Forget();
        }

        private async UniTaskVoid JumpAsync()
        {
            _player.Jump();
            await UniTask.NextFrame();
            _player.Fall();
        }
    }
}
