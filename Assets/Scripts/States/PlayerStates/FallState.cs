using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class FallState : GeneralState
    {
        private readonly Player _player;

        public FallState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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
