using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    private void Awake()
    {
        Instance = this;
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
        SceneManager.LoadScene(GameManager.SharedInstance.LastFinishedLevel + 1);
    }

    public void StartNewLevel()
    {
        GameManager.SharedInstance.ResetData();
        
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