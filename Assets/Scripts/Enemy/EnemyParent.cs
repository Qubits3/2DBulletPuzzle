using Core;
using UnityEngine;

namespace Enemy
{
    public class EnemyParent : MonoBehaviour
    {
        private GameObject _ragdollEnemy;
        private GameObject _enemy;

        public delegate void OnDestroyAction();

        public static event OnDestroyAction OnEnemyDestroy;

        private void Awake()
        {
            _ragdollEnemy = gameObject.FindGameObjectInParent("RagdollFox");
            _enemy = gameObject.FindGameObjectInParentWithTag("Enemy");

            _enemy.GetComponent<Enemy>().OnTriggerEnemy += OnEnemyHit;
        }

        private void OnEnemyHit()
        {
            _enemy.SetActive(false);
            _ragdollEnemy.SetActive(true);

            DestroyEnemy();
        }

        private void DestroyEnemy()
        {
            OnEnemyDestroy?.Invoke();
        }

        private void ApplyForceToHitPoint(GameObject enemy, Vector2 point)
        {
            if (enemy.GetComponent<Rigidbody2D>())
            {
                enemy.GetComponent<Rigidbody2D>().AddForceAtPosition(point.normalized * 1000, point);
            }
        }
    }
}