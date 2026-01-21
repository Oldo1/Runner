using Assets.Scripts.States.GameStates;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class GameStateMachine : StateMachine<GameBaseState>
    {
        public Type CurrentStateType => currentState.GetType();

        public void Init(GameManager game)
        {
            states = new Dictionary<Type, GameBaseState>()
            {
                { typeof(GameStarted), new GameStarted(game, this) },
                { typeof(GameOverState), new GameOverState(game, this) },
                { typeof(GameNotStarted), new GameNotStarted(game, this) },
                { typeof(GamePausedState), new GamePausedState(game, this) }
            };
            GameEvents.OnGameOver += OnGameOver;
            SwitchState<GameNotStarted>();
        }

        private void OnDestroy()
        {
            GameEvents.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            SwitchState<GameOverState>();
        }
    }
}
