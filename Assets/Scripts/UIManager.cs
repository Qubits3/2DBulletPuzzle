using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private enum Medal { Bronze, Silver, Gold }

    private GameObject _bulletPanel;
    private GameObject _nextLevelPanel;
    private GameObject _restartLevelPanel;
    private GameObject _inGameUI;
    private readonly GameObject[] _medals = new GameObject[Enum.GetNames(typeof(Medal)).Length];

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

            _nextLevelPanel = FindObjectInParent(_inGameUI, "NextLevelPanel");
            _restartLevelPanel = FindObjectInParent(_inGameUI, "RestartLevelPanel");
            _bulletPanel = GameObject.Find("BulletPanel");

            _nextLevelButton = FindObjectInParent(_inGameUI, "NextLevelButton").GetComponent<Button>();
            _mainMenuButton = FindObjectInParent(_inGameUI, "MainMenuButton").GetComponent<Button>();
            _restartButtonInNextLevelPanel =
                FindObjectInParent(_inGameUI, "RestartButtonInNextLevelPanel").GetComponent<Button>();
            _restartButtonInRestartGamePanel =
                FindObjectInParent(_inGameUI, "RestartButtonInRestartGamePanel").GetComponent<Button>();

            _medals[(int) Medal.Bronze] = FindObjectInParent(_inGameUI, "BronzeMedal");
            _medals[(int) Medal.Silver] = FindObjectInParent(_inGameUI, "SilverMedal");
            _medals[(int) Medal.Gold] = FindObjectInParent(_inGameUI, "GoldMedal");

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

        ShowMedal();
    }

    public void EnableRestartLevelPanel()
    {
        _restartLevelPanel.SetActive(true);
    }

    private void ShowMedal()
    {
        switch (_gameManager.GetBulletCount())
        {
            case 3:
                DeleteMedal(Medal.Gold);
                break;
            case 2:
                DeleteMedal(Medal.Gold);
                return;
            case 1:
                DeleteMedal(Medal.Gold);
                DeleteMedal(Medal.Silver);
                break;
            case 0:
                DeleteMedal(Medal.Gold);
                DeleteMedal(Medal.Silver);
                return;
        }
    }

    private void DeleteMedal(Medal medal)
    {
        Destroy(_medals[(int) medal]);
    }

    private GameObject FindObjectInParent(GameObject parent, string objectName)
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

    private bool IsThisSceneMainMenu()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    private void OnDestroy()
    {
        _bulletThrower.OnCreateBullet -= DrawBulletOnUI;
    }
}