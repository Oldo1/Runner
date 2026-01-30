using Assets.Scripts.States.GameStates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameStateMachine : StateMachine<GameBaseState>
    {
        public Type CurrentStateType => currentState.GetType();

        public void Init(GameManager game, Transform gameOverText, Transform restartHint, Transform gameStartHint)
        {
            states = new Dictionary<Type, GameBaseState>()
            {
                { typeof(GameStarted), new GameStarted(game, this) },
                { typeof(GameOverState), new GameOverState(gameOverText, restartHint, game ,this) },
                { typeof(GameNotStarted), new GameNotStarted(gameStartHint ,game, this) },
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
