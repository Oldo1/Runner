using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class GameObjectPool : IDisposable
    {
        private readonly ObjectPool<GameObject> _objectPool;
        private readonly GameObject _prefab;
        private Transform _parent;
        public GameObject Prefab => _prefab;
        public int CountActive => _objectPool.CountActive;

        public GameObjectPool(GameObject prefab, int initialCapacity, int maxSize)
        {
            _prefab = prefab;
            _objectPool = new ObjectPool<GameObject>(
                createFunc: CreateFunc,
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: obj => GameObject.Destroy(obj),
                collectionCheck: false,
                defaultCapacity: initialCapacity,
                maxSize: maxSize
            );
        }

        private GameObject CreateFunc()
        {
            if (_parent == null)
                return GameObject.Instantiate(_prefab);
            return GameObject.Instantiate(_prefab, _parent);
        }

        public GameObject Get()
        {
            return _objectPool.Get();
        }

        public GameObject Get(Transform parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be equals null");
            _parent = parent;
            var gameObject = Get();
            _parent = null;
            return gameObject;
        }

        public void Release(GameObject element)
        {
            _objectPool.Release(element);
        }

        public void Dispose()
        {
            _objectPool.Dispose();
        }
    }
}
