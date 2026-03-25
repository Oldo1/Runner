using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class FallState : GeneralState
    {
        private readonly Player _player;

        public FallState(Player player, PlayerStateMachine stateMachine, IInputHandler inputHandler) : base(player, stateMachine, inputHandler)
        {
            _player = player;
        }

        public override void OnEnter()
        {

            Debug.Log("Fall state");
            _player.Fall();
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
