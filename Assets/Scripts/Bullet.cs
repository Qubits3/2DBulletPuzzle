using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;

    private void Start()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        // transform.position += transform.up * (Time.deltaTime * bulletSpeed);
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}