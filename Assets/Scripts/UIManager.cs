using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _bulletPanel;
    private GameObject _nextLevelPanel;
    private GameObject _restartLevelPanel;
    private GameObject _inGameUI;

    private Button _nextLevelButton;
    private Button _mainMenuButton;
    private Button _continueButton;
    private Button _restartButtonInNextLevelPanel;
    private Button _restartButtonInRestartGamePanel;

    private BulletThrower _bulletThrower;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (IsThisSceneMainMenu())
        {
            _continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        }
        else
        {
            _inGameUI = GameObject.Find("InGameUI");

            _nextLevelPanel = FindObject(_inGameUI, "NextLevelPanel");
            _restartLevelPanel = FindObject(_inGameUI, "RestartLevelPanel");
            _bulletPanel = GameObject.Find("BulletPanel");

            _nextLevelButton = FindObject(_inGameUI, "NextLevelButton").GetComponent<Button>();
            _mainMenuButton = FindObject(_inGameUI, "MainMenuButton").GetComponent<Button>();
            _restartButtonInNextLevelPanel =
                FindObject(_inGameUI, "RestartButtonInNextLevelPanel").GetComponent<Button>();
            _restartButtonInRestartGamePanel =
                FindObject(_inGameUI, "RestartButtonInRestartGamePanel").GetComponent<Button>();

            _bulletThrower = FindObjectOfType<BulletThrower>();
            _bulletThrower.OnCreateBullet += DrawBulletOnUI;
        }
    }

    private void Start()
    {
        if (!IsThisSceneMainMenu())
        {
            _nextLevelButton.onClick.AddListener(LevelManager.Instance.LoadNextLevel);
            _mainMenuButton.onClick.AddListener(LevelManager.Instance.GoToMainMenu);
            _restartButtonInNextLevelPanel.onClick.AddListener(LevelManager.Instance.RestartLevel);
            _restartButtonInRestartGamePanel.onClick.AddListener(LevelManager.Instance.RestartLevel);
        }

        if (IsThisSceneMainMenu() && _gameManager.LastFinishedLevel != 0)
        {
            _continueButton.interactable = true;
        }
    }

    private void DrawBulletOnUI()
    {
        Destroy(_bulletPanel.GetComponentInChildren<Image>().gameObject);
    }

    public void EnableNextLevelPanel()
    {
        // ToDo: It throws MissingReferenceException after restarting game via RestartGamePanel
        _nextLevelPanel.SetActive(true);
    }

    public void EnableRestartLevelPanel()
    {
        _restartLevelPanel.SetActive(true);
    }

    private GameObject FindObject(GameObject parent, string objectName)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == objectName)
            {
                return t.gameObject;
            }
        }

        return null;
    }

    private GameObject GetChild(string childName)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Equals(childName))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    private bool IsThisSceneMainMenu()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    private void OnDestroy()
    {
        _bulletThrower.OnCreateBullet -= DrawBulletOnUI;
    }
}