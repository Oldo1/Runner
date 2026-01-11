using UnityEngine;

namespace Assets.Scripts.States
{
    public class GeneralState : PlayerState
    {
        private readonly Player _player;

        public GeneralState(Player player, StateMachine stateMachine) : base(stateMachine)
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
                else
                    stateMachine.SwitchState<MoveState>();
            }
            else if (!_player.IsOnGround && stateMachine.CurrentState != stateMachine.GetState<JumpState>() && stateMachine.CurrentState != stateMachine.GetState<FallState>())
                stateMachine.SwitchState<FallState>();
        }
    }
}
