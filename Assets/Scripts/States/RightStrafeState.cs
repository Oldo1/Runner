using UnityEngine;

namespace Assets.Scripts.States
{
    public class RightStrafeState : GeneralState
    {
        private readonly Player _player;

        public RightStrafeState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Right strafe");
            _player.StrafeRight();
        }
    }
}
