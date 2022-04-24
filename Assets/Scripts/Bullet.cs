using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;
    private Rigidbody2D _rigidbody2D;

    private int _bounceCount;
    [SerializeField] private int bounceCount = 5;

    private Vector3 _velocity;
    
    public float maxLength;
    private LineRenderer _lineRenderer;
    private Ray2D _ray;
    private RaycastHit2D _hit;
    private Vector3 _direction;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _ray = new Ray2D(transform.position, transform.up);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainingLength = maxLength;

        for (int i = 0; i < 2; i++)
        {
            _hit = Physics2D.Raycast(_ray.origin, _ray.direction, remainingLength, layerMask);
            if (_hit.collider)
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hit.point);
                remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                _ray = new Ray2D(_hit.point - _ray.direction * 0.01f, Vector2.Reflect(_ray.direction, _hit.normal));
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1,
                    _ray.origin + _ray.direction * remainingLength);
            }
        }
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

    private void ReflectBullet()
    {
        var reflected = Vector2.Reflect(_ray.direction, _hit.normal);
        _rigidbody2D.velocity = reflected * (bulletSpeed / 50);
        transform.rotation = Quaternion.FromToRotation(transform.up, reflected) * transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ReflectBullet();
        
        _bounceCount--;

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