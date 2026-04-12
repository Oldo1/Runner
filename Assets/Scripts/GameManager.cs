using Assets.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameStateMachine _gameStateMachine;

        private int _coinNumber;
        public bool IsPaused { get; private set; }

        public void Init(GameOverAnimation gameOverAnimation, ScaleLoopAnimation scaleLoopAnimation, UIInputHandler uiInputHandler, GameplayInputHandler gameplayInputHandler,
            PlayerMover playerMover, PlayerStateMachine playerStateMachine, ISegmentSpawnerAsync segmentsSpawnerAsync, CoinsRotator coinsRotator, 
            PlayerAnimationController playerAnimationController, SegmentsMover segmentsMover)
        {
            GameEvents.OnCollectCoin += IncreaseCoinNumber;
            _gameStateMachine.Init(this, gameOverAnimation, scaleLoopAnimation, uiInputHandler, gameplayInputHandler, playerMover, playerStateMachine, segmentsSpawnerAsync, coinsRotator,
                playerAnimationController, segmentsMover);
        }

        private void IncreaseCoinNumber(Coin coin)
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
            GameEvents.OnCollectCoin -= IncreaseCoinNumber;
        }
    }
}
