using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IBulletManager
{
    public int LastFinishedLevel { get; private set; }
    
    private bool _isLevelCompleted;
    private UIManager _uiManager;
    private BulletThrower _bulletThrower;

    private int _bulletCount = 5;

    private void Awake()
    {
        LoadData();

        _uiManager = FindObjectOfType<UIManager>();
        _bulletThrower = FindObjectOfType<BulletThrower>();

        _bulletThrower.OnCreateBullet += OnShot;
    }

    public void CompleteLevel()
    {
        _isLevelCompleted = true;
        _uiManager.EnableNextLevelPanel();

        SaveData();
    }

    private void OnOutOfAmmo()
    {
        _uiManager.EnableRestartLevelPanel();
    }
    
    public void OnBulletDestroy()
    {
        if (!_isLevelCompleted && _bulletCount == 0)
        {
            OnOutOfAmmo();
        }
    }

    public bool CanShoot()
    {
        return !_isLevelCompleted && IsThereEnoughBulletLeft();
    }
    
    private bool IsThereEnoughBulletLeft()
    {
        return _bulletCount > 0;
    }

    private void OnShot()
    {
        _bulletCount--;
    }

    public int GetBulletCount()
    {
        return _bulletCount;
    }

    private void OnDestroy()
    {
        _bulletThrower.OnCreateBullet -= OnShot;
    }

    [Serializable]
    public class Data
    {
        public int lastFinishedLevel;
    }

    private void SaveData()
    {
        var data = new Data
        {
            lastFinishedLevel = SceneManager.GetActiveScene().buildIndex
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadData()
    {
        var path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<Data>(json);

            LastFinishedLevel = data.lastFinishedLevel;
        }
        else
        {
            LastFinishedLevel = 0;
        }
    }

    public void ResetData()
    {
        var data = new Data
        {
            lastFinishedLevel = 0
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
}