using Core;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private GameObject _crosshair;
        private Camera _camera;
        private GameManager _gameManager;

        public delegate void TriggerAction();

        public event TriggerAction OnTriggerPlayer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") || collision.CompareTag("Obstacle"))
            {
                if (_gameManager)
                {
                    _gameManager.FailedLevel();
                }
            }
        }
        private void Awake()
        {
            _camera = Camera.main;
            _crosshair = GameObject.FindWithTag("Crosshair");
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            DrawCrosshair();
        }

        private void DrawCrosshair()
        {
            if (_gameManager)
            {
                if (_gameManager.CanShoot())
                {
                    _crosshair.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                }
            }
        }
    }
}