﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _nextLevelPanel;
    private GameObject _inGameUI;
    private Button _nextLevelButton;
    private Button _continueButton;
    private GameObject _bulletPanel;
    
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
            _nextLevelButton = FindObject(_inGameUI, "NextLevelButton").GetComponent<Button>();
            _bulletPanel = GameObject.Find("BulletPanel");
            
            _bulletThrower = FindObjectOfType<BulletThrower>();
            _bulletThrower.OnCreateBullet += DrawBulletOnUI;
        }
    }

    private void Start()
    {
        if (!IsThisSceneMainMenu())
        {
            _nextLevelButton.onClick.AddListener(LevelManager.Instance.LoadNextLevel);
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

    public void EnablePanel()
    {
        _nextLevelPanel.SetActive(true);
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