using UnityEngine;

namespace Assets.Scripts.States
{
    public class LeftStrafeState : GeneralState
    {
        private Player _player;

        public LeftStrafeState(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
