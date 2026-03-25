using Assets.Scripts.States.PlayerState;
using Assets.Scripts.States.PlayerStates;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerStateMachine : StateMachine<PlayerBaseState>
    {
        public Type CurrentStateType => currentState.GetType();

        public void Init(Player player, IInputHandler inputHandler, IDisable disableInputHandler, PlayerAnimationController playerAnimationController)
        {
            states = new Dictionary<Type, PlayerBaseState>()
            {
                { typeof(MoveState), new MoveState(player, this, inputHandler) },
                { typeof(LeftStrafeState), new LeftStrafeState(player, this, inputHandler) },
                { typeof(RightStrafeState), new RightStrafeState(player, this, inputHandler) },
                { typeof(JumpState), new JumpState(player, this, inputHandler, playerAnimationController) },
                { typeof(FallState), new FallState(player, this, inputHandler) },
                { typeof(DeadState), new DeadState(this, disableInputHandler) }
            };
            GameEvents.OnDie += SwitchState<DeadState>;
            SwitchState<MoveState>();
        }

        private void OnDestroy()
        {
            GameEvents.OnDie -= SwitchState<DeadState>;
        }
    }
}
