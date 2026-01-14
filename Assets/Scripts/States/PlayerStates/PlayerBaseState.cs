using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.States.PlayerState
{
    public class PlayerBaseState : BaseState
    {
        protected PlayerStateMachine stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
