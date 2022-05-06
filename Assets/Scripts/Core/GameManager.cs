using System;
using System.Collections;
using System.IO;
using Bullet;
using Obstacle;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour, IBulletManager
    {
        public int LastFinishedLevel { get; private set; }

        private bool _isLevelCompleted;
        private UIManager _uiManager;
        private ObstacleManager _obstacleManager;
        private BulletThrower _bulletThrower;

        private int _bulletCount = 5;

        private void Awake()
        {
            LoadData();

            _uiManager = FindObjectOfType<UIManager>();
            if (!Utils.IsThisSceneMainMenu())
            {
                _bulletThrower = FindObjectOfType<BulletThrower>();
                _obstacleManager = FindObjectOfType<ObstacleManager>();
                _bulletThrower.OnCreateBullet += OnShot;
            }
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
            StartCoroutine(_OnBulletDestroy());
        }

        private IEnumerator _OnBulletDestroy()
        {
            if (!_isLevelCompleted && _bulletCount == 0)
            {
                yield return new WaitForSeconds(0.5f);

                if (_obstacleManager.AreObstaclesGrounded())
                {
                    OnOutOfAmmo();
                }
                else
                {
                    _obstacleManager.CheckObstacles();
                }
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
            if (!Utils.IsThisSceneMainMenu())
            {
                _bulletThrower.OnCreateBullet -= OnShot;
            }
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
}