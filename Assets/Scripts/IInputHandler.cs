using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IInputHandler
    {
        public event Action<Vector2> OnStrafePerformed;
        public event Action OnJumpPerformed;
    }
}
