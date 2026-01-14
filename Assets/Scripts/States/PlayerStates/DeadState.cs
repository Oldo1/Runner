using Assets.Scripts.PlayerScripts;
using Assets.Scripts.States.PlayerState;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
    public class DeadState : PlayerBaseState
    {
        public DeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Dead");
        }
    }
}
