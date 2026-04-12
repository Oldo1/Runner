using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class GeneralState : PlayerBaseState
    {
        private readonly Player _player;
        private readonly IInputHandler _inputHandler;
        private Vector2 _strafeDirection;
        private bool _jumpPerformed;

        public GeneralState(Player player, PlayerStateMachine stateMachine, IInputHandler inputHandler) : base(stateMachine)
        {
            _player = player;
            _inputHandler = inputHandler;
        }

        public override void OnEnter()
        {
            _inputHandler.OnStrafePerformed += OnStrafePerformed;
            _inputHandler.OnJumpPerformed += OnJumpPerformed;
            base.OnEnter();
        }

        private void OnStrafePerformed(Vector2 strafeDirection)
        {
            if (!_player.IsStrafing && _player.IsOnGround)
                _strafeDirection = strafeDirection;
        }

        private void OnJumpPerformed()
        {
            _jumpPerformed = true;
        }

        public override void Update()
        {
            var currentState = stateMachine.CurrentStateType;
            if (!_player.IsStrafing && _player.IsOnGround)
            {
                if (_strafeDirection != Vector2.zero)
                {
                    if (_strafeDirection == Vector2.left && _player.CurrentLineNumber != _player.LeftLineNumber)
                        stateMachine.SwitchState<LeftStrafeState>();
                    else if (_strafeDirection == Vector2.right && _player.CurrentLineNumber != _player.RightLineNumber)
                        stateMachine.SwitchState<RightStrafeState>();
                }
                else if (_jumpPerformed)
                    stateMachine.SwitchState<JumpState>();
                else if (currentState != typeof(MoveState))
                    stateMachine.SwitchState<MoveState>();
            }
            else if (!_player.IsOnGround && currentState != typeof(JumpState) && currentState != typeof(FallState))
                stateMachine.SwitchState<FallState>();
        }

        public override void OnExit()
        {
            _inputHandler.OnStrafePerformed -= OnStrafePerformed;
            _inputHandler.OnJumpPerformed -= OnJumpPerformed;
            _strafeDirection = Vector2.zero;
            _jumpPerformed = false;
            base.OnExit();
        }
    }
}
