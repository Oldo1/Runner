namespace Assets.Scripts.States.GameStates
{
    public class GameStarted : GameBaseState
    {
        public GameStarted(GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public override void OnEnter()
        {
            game.StartGame();
            GameEvents.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            stateMachine.SwitchState<GameOverState>();
        }
    }
}
