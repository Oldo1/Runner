using System;
using System.Collections.Generic;

namespace Assets.Scripts.States
{
    public class StateMachine
    {
        Dictionary<Type, PlayerState> _playerStates;
        public PlayerState CurrentState { get; private set; }

        public StateMachine(Player player)
        {
            _playerStates = new Dictionary<Type, PlayerState>()
            {
                { typeof(MoveState), new MoveState(player, this) },
                { typeof(LeftStrafeState), new LeftStrafeState(player, this) },
                { typeof(RightStrafeState), new RightStrafeState(player, this) },
                { typeof(JumpState), new JumpState(player, this) },
                { typeof(FallState), new FallState(player, this)}
            };
            CurrentState = GetState<MoveState>();
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState.Update();
        }

        public T GetState<T>() where T : PlayerState
        {
            return (T)_playerStates[typeof(T)];
        }
       
        public void SwitchState<T>() where T : PlayerState
        {
            CurrentState.OnExit();
            CurrentState = GetState<T>();
            CurrentState.OnEnter();
        }
    }
}
