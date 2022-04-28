using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private GameManager _gameManager;

        private void Awake()
        {
            Instance = this;
        
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadLastRemainingLevel()
        {
            SceneManager.LoadScene(_gameManager.LastFinishedLevel + 1);
        }

        public void StartNewGame()
        {
            _gameManager.ResetData();
        
            LoadNextLevel();
        }

        public void QuitGame()
        {
            Application.Quit(0);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}