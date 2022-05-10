using System;
using System.Collections;
using Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        private enum Medal { Bronze, Silver, Gold }

        private GameObject _bulletPanel;
        private GameObject _nextLevelPanel;
        private GameObject _restartLevelPanel;
        private GameObject _inGameUI;
        private readonly GameObject[] _medals = new GameObject[Enum.GetNames(typeof(Medal)).Length];

        private Button _continueButton;

        private BulletThrower _bulletThrower;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();

            if (SceneManager.GetActiveScene().IsMainMenu())
            {
                _continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
            }
            else
            {
                _inGameUI = GameObject.Find("InGameUI");
                _nextLevelPanel = FindObjectInParent(_inGameUI, "NextLevelPanel");
                _restartLevelPanel = FindObjectInParent(_inGameUI, "RestartLevelPanel");
                _bulletPanel = GameObject.Find("BulletPanel");

                _medals[(int) Medal.Bronze] = FindObjectInParent(_inGameUI, "BronzeMedal");
                _medals[(int) Medal.Silver] = FindObjectInParent(_inGameUI, "SilverMedal");
                _medals[(int) Medal.Gold] = FindObjectInParent(_inGameUI, "GoldMedal");

                _bulletThrower = FindObjectOfType<BulletThrower>();
                _bulletThrower.OnCreateBullet += DrawBulletOnUI;
            }
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().IsMainMenu() && _gameManager.LastFinishedLevel != 0)
            {
                _continueButton.interactable = true;
            }
        }

        private void DrawBulletOnUI()
        {
            GameObject o = null;

            if (_bulletPanel)
            {
                foreach (var image in _bulletPanel.GetComponentsInChildren<Image>())
                {
                    o = image.gameObject;
                }
            }

            Destroy(o);
        }

        public void EnableNextLevelPanel()
        {
            StartCoroutine(_EnableNextLevelPanel());
        }

        private IEnumerator _EnableNextLevelPanel()
        {
            yield return new WaitForSeconds(1.0f);
            
            if (_nextLevelPanel)
            {
                _nextLevelPanel.SetActive(true);
            }

            ShowMedal();
        }

        public void EnableRestartLevelPanel()
        {
            if (_restartLevelPanel)
            {
                _restartLevelPanel.SetActive(true);
            }
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

        private void OnDestroy()
        {
            if (!SceneManager.GetActiveScene().IsMainMenu())
            {
                _bulletThrower.OnCreateBullet -= DrawBulletOnUI;
            }
        }
    }
}