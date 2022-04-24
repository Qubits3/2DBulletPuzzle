using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;
    private Rigidbody2D _rigidbody2D;

    private int _bounceCount;
    [SerializeField] private int bounceCount = 5;

    private Vector3 _velocity;

    Vector2 pre_vel;
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
        _velocity = Vector2.up * bulletSpeed;
        _rigidbody2D.AddRelativeForce(_velocity);
    }
    private void Update()
    {
        pre_vel = _rigidbody2D.velocity;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        _bounceCount--;
        var speed = pre_vel.magnitude;
        var direction = Vector3.Reflect(pre_vel.normalized, col.contacts[0].normal);
        _rigidbody2D.velocity = direction * Mathf.Max(speed, 3f);

        if (_bounceCount == 0)
        {
            DestroyBullet();
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