using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void OnDestroyAction();
    public static event OnDestroyAction OnEnemyDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            
            OnEnemyDestroy?.Invoke();
        }
    }
}
