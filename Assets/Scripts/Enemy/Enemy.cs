using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Vector3 bulletPos;

        public delegate void TriggerAction();

        public event TriggerAction OnTriggerEnemy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                bulletPos = collision.gameObject.transform.position;

                OnTriggerEnemy?.Invoke();
            } else if (collision.CompareTag("Obstacle"))
            {
                OnTriggerEnemy?.Invoke();
            }
        }
    }
}