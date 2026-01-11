using UnityEngine;

namespace Assets.Scripts.States
{
    public class MoveState : GeneralState
    {
        public MoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
