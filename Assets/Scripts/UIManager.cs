using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _nextLevelPanel;
    private GameObject _inGameUI;
    private Button _nextLevelButton;
    private Button _continueButton;

    private void Awake()
    {
        if (IsThisSceneMainMenu())
        {
            _continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        }
        else
        {
            _inGameUI = GameObject.Find("InGameUI");
            _nextLevelPanel = FindObject(_inGameUI, "NextLevelPanel");
            _nextLevelButton = FindObject(_inGameUI, "NextLevelButton").GetComponent<Button>();
        }
    }

    private void Start()
    {
        if (!IsThisSceneMainMenu())
        {
            _nextLevelButton.onClick.AddListener(LevelManager.Instance.LoadNextLevel);
        }

        print(GameManager.SharedInstance.LastFinishedLevel);
        
        if (IsThisSceneMainMenu() && GameManager.SharedInstance.LastFinishedLevel != 0)
        {
            _continueButton.interactable = true;
        }
    }

    public void EnablePanel()
    {
        _nextLevelPanel.SetActive(true);
    }

    private GameObject FindObject(GameObject parent, string _name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == _name)
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
}