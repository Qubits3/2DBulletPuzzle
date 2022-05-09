using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public delegate void TriggerAction();

        public event TriggerAction OnTriggerEnemy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") || collision.CompareTag("Obstacle"))
            {
                OnTriggerEnemy?.Invoke();
            }
        }
    }
}