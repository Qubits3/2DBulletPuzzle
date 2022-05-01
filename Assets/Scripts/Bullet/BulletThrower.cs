using Core;
using UnityEngine;

namespace Bullet
{
    public class BulletThrower : MonoBehaviour
    {
        private Transform _bulletTransform;
        private GameManager _gameManager;

        public delegate void BulletAction();

        public event BulletAction OnCreateBullet;

        private void Awake()
        {
            _bulletTransform = GameObject.Find("BulletSpawnPosition").transform;
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnMouseUp()
        {
            if (_gameManager.CanShoot())
            {
                CreateBullet();
            }
        }

        private void CreateBullet()
        {
            var bullet = ObjectPooler.SharedInstance.GetPooledObject();
            bullet.transform.position = _bulletTransform.position;
            bullet.transform.rotation = _bulletTransform.rotation;
            bullet.SetActive(true);

            OnCreateBullet?.Invoke();
        }
    }
}