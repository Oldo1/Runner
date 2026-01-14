using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameNotStarted : GameBaseState
    {
        public GameNotStarted(GameManager game, GameStateMachine stateMachine) : base(game, stateMachine)
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SwitchState<GameStarted>();
            }
        }
    }
}
