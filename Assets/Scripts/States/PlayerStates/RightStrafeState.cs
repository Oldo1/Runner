using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class RightStrafeState : GeneralState
    {
        private readonly Player _player;

        public RightStrafeState(Player player, PlayerStateMachine stateMachine, IInputHandler inputHandler) : base(player, stateMachine, inputHandler)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Right strafe");
            _player.StrafeRight();
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
