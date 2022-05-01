using Core;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private GameObject _crosshair;
        private Camera _camera;
        private GameManager _gameManager;

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
            if (_gameManager.CanShoot())
            {
                _crosshair.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            }
        }
    }
}