using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameNotStarted : GameBaseState
    {
        private readonly ScaleLoopAnimation _scaleLoopAnimation;
        private readonly CoinsRotator _coinsRotator;
        private readonly UIInputHandler _tapUIInputHandler;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly SegmentsMover _segmentsMover;

        public GameNotStarted(ScaleLoopAnimation scaleLoopAnimation, GameManager game, GameStateMachine stateMachine, UIInputHandler notGameStartedUIInputHandler,
            PlayerMover playerMover, PlayerStateMachine playerStateMachine, CoinsRotator coinsRotator, SegmentsMover segmentsMover) : base(game, stateMachine)
        {
            _scaleLoopAnimation = scaleLoopAnimation;
            _coinsRotator = coinsRotator;
            _tapUIInputHandler = notGameStartedUIInputHandler;
            _playerMover = playerMover;
            _playerStateMachine = playerStateMachine;
            _segmentsMover = segmentsMover;
        }

        public override void OnEnter()
        {
            _scaleLoopAnimation.Play(new Vector3(1.15f, 1.15f, 0f), 0.5f);
            _tapUIInputHandler.Enable();
            _coinsRotator.enabled = false;
            _playerMover.enabled = false;
            _playerStateMachine.enabled = false;
            _segmentsMover.enabled = false;
        }

        public override void Update()
        {
            if (_tapUIInputHandler.WasTap())
            {
                stateMachine.SwitchState<GameStarted>();
            }
        }

        public override void OnExit()
        {
            _scaleLoopAnimation.Kill();
            _tapUIInputHandler.Disable();
        }
    }
}
