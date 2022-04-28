using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public delegate void OnDestroyAction();

        public static event OnDestroyAction OnEnemyDestroy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") || collision.CompareTag("Obstacle"))
            {
                DestroyEnemy();

                OnEnemyDestroy?.Invoke();
            }
        }

        private void DestroyEnemy()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}