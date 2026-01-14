using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : GameBaseState
    {
        public GameOverState(GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                game.RestartGame();
            }
        }
    }
}
