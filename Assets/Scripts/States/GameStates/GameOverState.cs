using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : GameBaseState
    {
        private readonly GameOverAnimation _gameOverAnimation;
        private readonly SegmentsSpawnerAsync _segmentsSpawner;
        private readonly CoinsRotator _coinsRotator;
        private readonly PlayerAnimationController _playerAnimationController;
        private readonly SegmentsMover _segmentsMover;
        private readonly UIInputHandler _inputHandler;

        public GameOverState(GameOverAnimation gameOverAnimation, GameManager game, GameStateMachine stateMachine, SegmentsSpawnerAsync segmentsSpawnerAsync,
            CoinsRotator coinsRotator, PlayerAnimationController playerAnimationController, SegmentsMover segmentsMover, UIInputHandler uiInputHandler) : base(game, stateMachine)
        {
            _gameOverAnimation = gameOverAnimation;
            _segmentsSpawner = segmentsSpawnerAsync;
            _coinsRotator = coinsRotator;
            _playerAnimationController = playerAnimationController;
            _segmentsMover = segmentsMover;
            _inputHandler = uiInputHandler;
        }

        public override void OnEnter()
        {
            game.GameOver();
            _inputHandler.Enable();
            _gameOverAnimation.Play();
            _segmentsSpawner.StopSpawning();
            _playerAnimationController.Pause();
            _coinsRotator.enabled = false;
            _segmentsMover.enabled = false;
        }

        public override void Update()
        {
            if (_inputHandler.WasTap())
                game.RestartGame();
        }

        public override void OnExit()
        {
            _inputHandler.Disable();
            _gameOverAnimation.Kill();
        }
    }
}
