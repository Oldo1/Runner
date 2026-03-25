using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class LeftStrafeState : GeneralState
    {
        private readonly Player _player;

        public LeftStrafeState(Player player, PlayerStateMachine stateMachine, IInputHandler inputHandler) : base(player, stateMachine, inputHandler)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Left strafe");
            _player.StrafeLeft();
            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
