using System;
using UnityEngine;

namespace Assets.Scripts
{
    public static class GameEvents
    {
        public static event Action OnDie;
        public static event Action OnGameOver;
        public static event Action OnGameRestart;
        public static event Action OnStartGame;
        public static event Action<GameObject> OnCollectCoin;
        public static event Action<GameObject> OnSpawnSegment;
        public static event Action<GameObject> OnDestroySegment;
        public static event Action<int> OnChangeCoinNumber;

        public static void InvokeOnDieEvent()
        {
            OnDie?.Invoke();
        }

        public static void InvokeOnGameOverEvent()
        {
            OnGameOver?.Invoke();
        }

        public static void InvokeOnChangeCoinNumber(int coinsNumber)
        {
            OnChangeCoinNumber?.Invoke(coinsNumber);
        }

        public static void InvokeOnOnStartGame()
        {
            OnStartGame?.Invoke();
        }

        public static void InvokeOnSpawnSegment(GameObject segment)
        {
            OnSpawnSegment?.Invoke(segment);
        }

        public static void InvokeOnDestroySegment(GameObject segment)
        {
            OnDestroySegment?.Invoke(segment);
        }

        public static void InvokeOnCollectCoinEvent(GameObject coin)
        {
            OnCollectCoin?.Invoke(coin);
        }

        public static void InvokeOnGameRestart()
        {
            OnGameRestart?.Invoke();
        }
    }
}
