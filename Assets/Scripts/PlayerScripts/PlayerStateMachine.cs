using Assets.Scripts.States.PlayerState;
using Assets.Scripts.States.PlayerStates;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerStateMachine : StateMachine<PlayerBaseState>
    {
        public Type CurrentStateType => currentState.GetType();

        public void Init(Player player)
        {
            states = new Dictionary<Type, PlayerBaseState>()
            {
                { typeof(MoveState), new MoveState(player, this) },
                { typeof(LeftStrafeState), new LeftStrafeState(player, this) },
                { typeof(RightStrafeState), new RightStrafeState(player, this) },
                { typeof(JumpState), new JumpState(player, this) },
                { typeof(FallState), new FallState(player, this) },
                { typeof(DeadState), new DeadState(this) }
            };
            GameEvents.OnDie += SwitchState<DeadState>;
            GameEvents.OnStartGame += Enable;
            GameEvents.OnPause += Disable;
            SwitchState<MoveState>();
        }

        private void Enable()
        {
            enabled = true;
        }

        private void Disable()
        {
            enabled = false;
        }

        private void OnDestroy()
        {
            GameEvents.OnDie -= SwitchState<DeadState>;
            GameEvents.OnStartGame -= Enable;
            GameEvents.OnPause -= Disable;
        }
    }
}
