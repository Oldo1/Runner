using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    [Serializable]
    public class SerializedDictionary<T1, T2> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyValuePair<T1, T2>> _keyValuePairs;
        [SerializeField] private Dictionary<T1, T2> _dictionary;
        public IDictionary<T1, T2> Dictionary => _dictionary;

        public bool TryGetValue(T1 key, out T2 value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public void OnAfterDeserialize()
        {
            if (_dictionary == null)
                _dictionary = new Dictionary<T1, T2>();
            else
                _dictionary.Clear();

            foreach (var keyValuePair in _keyValuePairs)
            {
                _dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public void OnBeforeSerialize()
        {
            _keyValuePairs.Clear();

            foreach (var keyValuePair in _dictionary)
                _keyValuePairs.Add(new KeyValuePair<T1, T2>(keyValuePair.Key, keyValuePair.Value));
        }
    }

    [Serializable]
    public struct KeyValuePair<T1, T2>
    {
        [SerializeField] public T1 Key;
        [SerializeField] public T2 Value;

        public KeyValuePair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }
    }
}