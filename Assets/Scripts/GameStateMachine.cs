using Assets.Scripts.PlayerScripts;
using Assets.Scripts.States.GameStates;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class GameStateMachine : StateMachine<GameBaseState>
    {
        public void Init(GameManager game, GameOverAnimation gameOverAnimation, ScaleLoopAnimation scaleLoopAnimation, UIInputHandler uiInputHandler, 
            GameplayInputHandler gameplayInputHandler, PlayerMover playerMover, PlayerStateMachine playerStateMachine, SegmentsSpawnerAsync segmentsSpawnerAsync,
            CoinsRotator coinsRotator, PlayerAnimationController playerAnimationController, SegmentsMover segmentsMover)
        {
            states = new Dictionary<Type, GameBaseState>()
            {
                { typeof(GameStarted), new GameStarted(game, this, gameplayInputHandler, uiInputHandler, playerMover, playerStateMachine, segmentsSpawnerAsync) },
                { typeof(GameOverState), new GameOverState(gameOverAnimation, game ,this, segmentsSpawnerAsync, coinsRotator, playerAnimationController, segmentsMover, uiInputHandler) },
                { typeof(GameNotStarted), new GameNotStarted(scaleLoopAnimation ,game, this, uiInputHandler, playerMover, playerStateMachine, coinsRotator, segmentsMover) },
            };
            GameEvents.OnDie += OnDie;
            SwitchState<GameNotStarted>();
        }

        private void OnDestroy()
        {
            GameEvents.OnDie -= OnDie;
        }

        private void OnDie()
        {
            SwitchState<GameOverState>();
        }
    }
}
