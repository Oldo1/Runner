using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GamePausedState : GameBaseState
    {
        public GamePausedState(GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public override void OnEnter()
        {
            game.Pause();
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.SwitchState<GameStarted>();
            }
        }
    }
}
