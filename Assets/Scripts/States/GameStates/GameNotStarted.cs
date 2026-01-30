using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameNotStarted : GameBaseState
    {
        private readonly ScaleLoopAnimation _scaleLoopAnimation;
        private readonly CoinsRotator _coinsRotator;

        public GameNotStarted(Transform gameStartHint, GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
            _scaleLoopAnimation = new ScaleLoopAnimation(gameStartHint);
            _coinsRotator = ServiceLocator.Get<CoinsRotator>();
        }

        public override void OnEnter()
        {
            _scaleLoopAnimation.Play(new Vector3(1.15f, 1.15f, 0f), 0.5f);
            _coinsRotator.enabled = false;
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SwitchState<GameStarted>();
            }
        }

        public override void OnExit()
        {
            _scaleLoopAnimation.Kill();
        }
    }
}
