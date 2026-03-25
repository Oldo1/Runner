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

        public void Play(Vector3 endValue, float duration)
        {
            _transform.DOScale(endValue, duration).SetLoops(-1, LoopType.Yoyo);
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
