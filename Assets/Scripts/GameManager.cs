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
        public bool IsPaused { get; private set; }

        public static GameManager Instance { get; private set; } 

        public void Init(Transform gameOverText, Transform restartHint, Transform gameStartHint)
        {
            if (Instance == null)
                Instance = this;

            GameEvents.OnCollectCoin += _ => IncreaseCoinNumber();
            _gameStateMachine.Init(this, gameOverText, restartHint, gameStartHint);
        }

        private void IncreaseCoinNumber()
        {
            _coinNumber++;
            GameEvents.InvokeOnChangeCoinNumber(_coinNumber);
        }

        public void GameOver()
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
            IsPaused = true;
            GameEvents.InvokeOnPauseGame();
        }

        public void Resume()
        {
            IsPaused = false;
            GameEvents.InvokeOnResumeGame();
        }

        private void OnDestroy()
        {
            GameEvents.OnDie -= GameOver;
            GameEvents.OnCollectCoin -= _ => IncreaseCoinNumber();
        }
    }
}
