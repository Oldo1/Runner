namespace Assets.Scripts.States.GameStates
{
    public class GameBaseState : BaseState
    {
        protected readonly GameManager game;
        protected readonly GameStateMachine stateMachine;

        public GameBaseState(GameManager game, GameStateMachine stateMachine)
        {
            this.game = game;
            this.stateMachine = stateMachine;
        }
    }
}
