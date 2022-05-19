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
            bullet.transform.SetPositionAndRotation(_bulletTransform.position, _bulletTransform.rotation);
            bullet.SetActive(true);

            OnCreateBullet?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                DestroyBullet(other.gameObject);
            }
        }
        
        private void DestroyBullet(GameObject bullet)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            bullet.SetActive(false);
        }
    }
}