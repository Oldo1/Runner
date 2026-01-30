using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameStarted : GameBaseState
    {
        private readonly SegmentsSpawnerAsync _segmentsSpawner;
        private readonly CoinsRotator _coinsRotator;
        private readonly PlayerAnimationController _animationController;
        private readonly PlayerMover _playerMover;
        private readonly SegmentsMover _segmentsMover;
        private readonly PlayerStateMachine _playerStateMachine;

        public GameStarted(GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
            _segmentsSpawner = ServiceLocator.Get<SegmentsSpawnerAsync>();
            _coinsRotator = ServiceLocator.Get<CoinsRotator>();
            _animationController = ServiceLocator.Get<PlayerAnimationController>();
            _playerMover = ServiceLocator.Get<PlayerMover>();
            _segmentsMover = ServiceLocator.Get<SegmentsMover>();
            _playerStateMachine = ServiceLocator.Get<PlayerStateMachine>();
        }

        public override void OnEnter()
        {
            game.StartGame();
            _segmentsSpawner.StartSpawning();
            _animationController.Play();
            _playerMover.enabled = true;
            _coinsRotator.enabled = true;
            _segmentsMover.enabled = true;
            _playerStateMachine.enabled = true;
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!game.IsPaused)
                {
                    game.Pause();
                    _coinsRotator.enabled = false;
                    _playerMover.enabled = false;
                    _segmentsMover.enabled = false;
                    _playerStateMachine.enabled = false;
                    _animationController.Pause();
                }
                else
                {
                    game.Resume();
                    _coinsRotator.enabled = true;
                    _playerMover.enabled = true;
                    _segmentsMover.enabled = true;
                    _playerStateMachine.enabled = true;
                    _animationController.Play();
                }
            }
        }
    }
}
