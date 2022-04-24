using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _nextLevelPanel;
    private GameObject _inGameUI;
    private Button _nextLevelButton;

    private void Awake()
    {
        _inGameUI = GameObject.Find("InGameUI");
        _nextLevelPanel = FindObject(_inGameUI, "NextLevelPanel");
        _nextLevelButton = FindObject(_inGameUI, "NextLevelButton").GetComponent<Button>();
    }

    private void Start()
    {
        _nextLevelButton.onClick.AddListener(LevelManager.Instance.NextScene);
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
}