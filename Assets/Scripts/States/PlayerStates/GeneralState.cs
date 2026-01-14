using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class GeneralState : PlayerBaseState
    {
        private readonly Player _player;

        public GeneralState(Player player, PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _player = player;
        }

        public override void Update()
        {
            if (!_player.IsStrafing && _player.IsOnGround)
            {
                if (Input.GetKeyDown(KeyCode.A) && _player.CurrentLineNumber != _player.LeftLineNumber)
                    stateMachine.SwitchState<LeftStrafeState>();
                else if (Input.GetKeyDown(KeyCode.D) && _player.CurrentLineNumber != _player.RightLineNumber)
                    stateMachine.SwitchState<RightStrafeState>();
                else if (Input.GetKeyDown(KeyCode.Space))
                    stateMachine.SwitchState<JumpState>();
                else if (stateMachine.CurrentStateType != typeof(MoveState))
                    stateMachine.SwitchState<MoveState>();
            }
            else if (!_player.IsOnGround && !_player.IsGravityHandling)
                stateMachine.SwitchState<FallState>();
        }
    }
}
