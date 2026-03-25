using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.PlayerState
{
    public class MoveState : GeneralState
    {
        public MoveState(Player player, PlayerStateMachine stateMachine, IInputHandler inputHandler) : base(player, stateMachine, inputHandler)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Move State");
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
