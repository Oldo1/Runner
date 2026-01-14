using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class MoveState : GeneralState
    {
        public MoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Move State");
        }

        public override void Update()
        {
            base.Update();
        }

    }
}
