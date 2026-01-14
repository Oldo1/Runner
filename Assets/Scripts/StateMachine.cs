using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class StateMachine<T> : MonoBehaviour where T : BaseState
    {
        protected T currentState;
        protected Dictionary<Type, T> states;

        public T1 GetState<T1>() where T1 : T
        {
            return (T1)states[typeof(T1)];
        }

        public void SwitchState<T1>() where T1 : T
        {
            currentState?.OnExit();
            currentState = states[typeof(T1)];
            currentState?.OnEnter();
        }

        private void Update()
        {
            currentState?.Update();
        }
    }
}
