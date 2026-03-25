using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IInputHandler
    {
        //public bool TryGetStrafeDirection(out Vector2 strafeDirection);
        public event Action<Vector2> OnStrafePerformed;
        public event Action OnJumpPerformed;
    }
}
