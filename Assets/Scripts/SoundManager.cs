using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts 
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<SoundType, AudioClip> _sounds;
        [SerializeField] private AudioSource _audioSource;

        private static SoundManager _instance;

        public void Init()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            GameEvents.OnCollectCoin += OnCollectCoin;
            DontDestroyOnLoad(gameObject);
        }

        private void OnCollectCoin(Coin coin)
        {
            PlaySound(SoundType.CoinCollect);
        }

        public void PlaySound(SoundType soundType)
        {
            if (_sounds.TryGetValue(soundType, out AudioClip audioClip))
                _audioSource.PlayOneShot(audioClip);
            else
                throw new KeyNotFoundException();
        }

        public void OnDestroy()
        {
            GameEvents.OnCollectCoin -= OnCollectCoin;
        }
    }
}
