using Assets.Scripts.States;

namespace Assets.Scripts
{
    public class PlayerState
    {
        protected StateMachine stateMachine;

        public PlayerState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {

        }
    }
}
