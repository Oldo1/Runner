using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        private int _coinNumber;
        private bool _isStarted;
        private bool _isGameOver;

        public void Init()
        {
            GameEvents.OnDie += GameOver;
            GameEvents.OnCollectCoin += _ => IncreaseCoinNumber();
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

        private void StartGame()
        {
            _isStarted = true;
            GameEvents.InvokeOnOnStartGame();   
        }

        private void RestartGame()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!_isStarted)
                    StartGame();
                else if (_isGameOver)
                    RestartGame();
            }
        }

        private void OnApplicationQuit()
        {
            GameEvents.OnDie -= GameOver;
            GameEvents.OnCollectCoin -= _ => IncreaseCoinNumber();
        }
    }
}
