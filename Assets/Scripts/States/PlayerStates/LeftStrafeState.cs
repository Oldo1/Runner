using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class LeftStrafeState : GeneralState
    {
        private readonly Player _player;

        public LeftStrafeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Left strafe");
            _player.StrafeLeft();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
