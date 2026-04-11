using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScaleLoopAnimation : IDisposable
    {
        private readonly Transform _transform;

        public ScaleLoopAnimation(Transform transform)
        {
            _transform = transform;
        }

        public void Play()
        {
            _transform.DOScale(new Vector3(1.15f, 1.15f, 0), 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        public void Kill()
        {
            _transform.DOKill();
        }

        public void Dispose()
        {
            Kill();
        }
    }
}
