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

        public GameOverState(Transform gameOverText, Transform restartHint, GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
            _gameOverAnimation = new GameOverAnimation(gameOverText, restartHint);
            _segmentsSpawner = ServiceLocator.Get<SegmentsSpawnerAsync>();
            _coinsRotator = ServiceLocator.Get<CoinsRotator>();
            _playerAnimationController = ServiceLocator.Get<PlayerAnimationController>();
            _segmentsMover = ServiceLocator.Get<SegmentsMover>();
        }

        public override void OnEnter()
        {
            game.GameOver();
            
            _gameOverAnimation.Play();
            _segmentsSpawner.StopSpawning();
            _playerAnimationController.Pause();
            _coinsRotator.enabled = false;
            _segmentsMover.enabled = false;
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                game.RestartGame();
            }
        }

        public override void OnExit()
        {
            _gameOverAnimation.Kill();
        }
    }
}
