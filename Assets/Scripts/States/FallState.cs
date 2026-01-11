using UnityEngine;

namespace Assets.Scripts.States
{
    public class FallState : GeneralState
    {
        private readonly Player _player;

        public FallState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Fall state"); 
            _player.Fall();
        }
    }
}
