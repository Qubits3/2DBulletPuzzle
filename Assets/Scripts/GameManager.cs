using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance { get; private set; }
    public bool IsLevelCompleted { get; private set; }
    public int LastFinishedLevel { get; private set; }
    private UIManager _uiManager;
    private BulletThrower _bulletThrower;

    public int BulletCount { get; private set; } = 5;

    private void Awake()
    {
        SharedInstance = this;

        LoadData();

        _uiManager = FindObjectOfType<UIManager>();
        _bulletThrower = FindObjectOfType<BulletThrower>();

        _bulletThrower.OnCreateBullet += OnShot;
    }

    public void CompleteLevel()
    {
        IsLevelCompleted = true;
        _uiManager.EnablePanel();

        SaveData();
    }

    private void OnShot()
    {
        BulletCount--;
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

    private void OnDestroy()
    {
        _bulletThrower.OnCreateBullet -= OnShot;
    }
}