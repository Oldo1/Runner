using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class GameObjectPoolService
    {
        private readonly static Dictionary<GameObject, GameObjectPool> _gameObjectPools = new();
        private readonly static Dictionary<GameObject, GameObject> _spawnedGameObjectsMap = new();

        public static void CreatePool(GameObject prefab, int initialCapacity, int maxSize)
        {
            if (initialCapacity > maxSize)
                throw new ArgumentException("initial capacity can't be greater than max size");
            if (_gameObjectPools.ContainsKey(prefab))
                throw new InvalidOperationException("pool already created for this prefab");
            var objectPool = new GameObjectPool(prefab, initialCapacity, maxSize);
            _gameObjectPools.Add(prefab, objectPool);
        }

        public static GameObject Spawn(GameObject prefab, Transform parent = null)
        {
            if (!_gameObjectPools.ContainsKey(prefab))
                throw new KeyNotFoundException("no pool found for this prefab");
            var pool = GetObjectPool(prefab);
            var spawnedObject = parent == null ? pool.Get() : pool.Get(parent);
            _spawnedGameObjectsMap.Add(spawnedObject, prefab);
            return spawnedObject;
        }

        public static GameObject Spawn(GameObject prefab, Vector3 position)
        {
            var spawnedObject = Spawn(prefab);
            spawnedObject.transform.position = position;
            return spawnedObject;
        }

        public static void Despawn(GameObject gameObject)
        {
            if (!IsSpawned(gameObject))
                throw new KeyNotFoundException("this game object was not spawned from any pool");
            var prefab = _spawnedGameObjectsMap[gameObject];
            var pool = GetObjectPool(prefab);
            pool.Release(gameObject);
            _spawnedGameObjectsMap.Remove(gameObject);
        }

        public static GameObjectPool GetObjectPool(GameObject prefab)
        {
            if (!_gameObjectPools.ContainsKey(prefab))
                throw new KeyNotFoundException("no pool found for this prefab");

            return _gameObjectPools[prefab];
        }

        public static bool IsSpawned(GameObject gameObject)
        {
            return _spawnedGameObjectsMap.ContainsKey(gameObject);
        }

        public static void Clear()
        {
            _gameObjectPools.Clear();
        }
    }
}
