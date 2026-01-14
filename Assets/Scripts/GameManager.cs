using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameStateMachine _gameStateMachine;

        private int _coinNumber;

        public void Init()
        {
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

        private void OnDestroy()
        {
            GameEvents.OnDie -= GameOver;
            GameEvents.OnCollectCoin -= _ => IncreaseCoinNumber();
        }
    }
}
