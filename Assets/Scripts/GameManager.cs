using Assets.Scripts.States.GameStates;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameStateMachine _gameStateMachine;

        private int _coinNumber;
        public Type CurrentStateType => _gameStateMachine.CurrentStateType;

        public static GameManager Instance { get; private set; } 

        public void Init()
        {
            if (Instance == null)
                Instance = this;

            GameEvents.OnDie += GameOver;
            GameEvents.OnCollectCoin += _ => IncreaseCoinNumber();
            _gameStateMachine.Init(this);
        }

        private void IncreaseCoinNumber()
        {
            _coinNumber++;
            GameEvents.InvokeOnChangeCoinNumber(_coinNumber);
        }

        private void GameOver()
        {
            Debug.Log("GameOver");
            GameEvents.InvokeOnGameOverEvent();
        }

        public void StartGame()
        {
            GameEvents.InvokeOnOnStartGame();   
        }

        public void RestartGame()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        public void Pause()
        {
            GameEvents.InvokeOnPauseGame();
        }

        private void OnDestroy()
        {
            GameEvents.OnDie -= GameOver;
            GameEvents.OnCollectCoin -= _ => IncreaseCoinNumber();
        }
    }
}
