using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsLevelCompleted { get; private set; }
    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void CompleteLevel()
    {
        IsLevelCompleted = true;
        _uiManager.EnablePanel();
    }
}