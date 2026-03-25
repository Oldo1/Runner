using Assets.Scripts.PlayerScripts;
using Assets.Scripts.States.PlayerState;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
    public class DeadState : PlayerBaseState
    {
        private readonly IDisable _playerInputHandler;

        public DeadState(PlayerStateMachine stateMachine, IDisable playerInputHandler) : base(stateMachine)
        {
            _playerInputHandler = playerInputHandler;
        }

        public override void OnEnter()
        {
            Debug.Log("Dead");
            _playerInputHandler.Disable();
        }
    }
}
