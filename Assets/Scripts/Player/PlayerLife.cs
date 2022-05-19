using Core;
using UnityEngine;

namespace Player
{
    public class PlayerLife : MonoBehaviour
    {
        public Vector3 bulletPos;

        private GameObject _ragdollPlayer;
        private GameObject _player;
        private GameManager _gameManager;
        private void Awake()
        {
            _ragdollPlayer = gameObject.FindGameObjectInParent("Ragdoll Bunny");
            _player = gameObject.FindGameObjectInParent("Bunny");

            _gameManager = FindObjectOfType<GameManager>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") || collision.CompareTag("Obstacle"))
            {
                if (_gameManager)
                {
                    bulletPos = collision.transform.position;
                    _player.SetActive(false);
                    _ragdollPlayer.SetActive(true);
                    _gameManager.FailedLevel();
                }
            }
        }
    }
}
