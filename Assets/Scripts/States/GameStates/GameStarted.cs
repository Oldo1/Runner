using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.States.GameStates
{
    public class GameStarted : GameBaseState
    {
        private readonly GameplayInputHandler _gameplayInputHandler;
        private readonly UIInputHandler _uiInputHandler;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly SegmentsSpawnerAsync _segmentsSpawnerAsync;

        public GameStarted(GameManager game, GameStateMachine stateMachine, GameplayInputHandler gameplayerInputHandler, UIInputHandler uiInputHandler,
            PlayerMover playerMover, PlayerStateMachine playerStateMachine, SegmentsSpawnerAsync segmentsSpawnerAsync) : base(game, stateMachine)
        {
            _uiInputHandler = uiInputHandler;
            _playerMover = playerMover;
            _playerStateMachine = playerStateMachine;
            _gameplayInputHandler = gameplayerInputHandler;
            _segmentsSpawnerAsync = segmentsSpawnerAsync;
        }

        public override void OnEnter()
        {
            game.StartGame();
            _playerMover.enabled = true;
            _playerStateMachine.enabled = true;
            _gameplayInputHandler.Enable();
            _segmentsSpawnerAsync.StartSpawning();
        }

        public override void Update()
        {
            if (game.IsPaused && _uiInputHandler.WasTap())
            {
                game.Resume();
                _uiInputHandler.Disable();
            }
        }

        public override void OnExit()
        {
            _gameplayInputHandler.Disable();
        }
    }
}
