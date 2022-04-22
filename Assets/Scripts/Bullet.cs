using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;
    private Rigidbody2D _rigidbody2D;

    private int _bounceCount;
    [SerializeField] private int bounceCount = 5;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _bounceCount = bounceCount + 1;
        MoveBullet();
    }

    private void MoveBullet()
    {
        _rigidbody2D.AddRelativeForce(Vector2.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _bounceCount--;

        if (_bounceCount == 0)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) return;

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        _rigidbody2D.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}