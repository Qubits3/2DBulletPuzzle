using Core;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(LineRenderer))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 1000.0f;
        private Rigidbody2D _rigidbody2D;

        private int _bounceCount;
        private const int BounceCount = 5;

        private Vector3 _velocity;

        public float maxLength;
        private LineRenderer _lineRenderer;
        private Ray2D _ray;
        private RaycastHit2D _hit;

        private IBulletManager _iBulletManager;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _lineRenderer = GetComponent<LineRenderer>();

            _iBulletManager = FindObjectOfType<GameManager>();
        }

        private void FixedUpdate()
        {
            DrawBulletPrediction();
        }

        private void DrawBulletPrediction()
        {
            var transform1 = transform;
            var position = transform1.position;
            _ray = new Ray2D(position, transform1.up);

            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, position);

            float remainingLength = maxLength;

            for (int i = 0; i < 2; i++)
            {
                _hit = Physics2D.Raycast(_ray.origin, _ray.direction, remainingLength,
                    LayerMask.GetMask("Terrain", "Enemy", "Obstacle"));
                if (_hit.collider)
                {
                    var positionCount = _lineRenderer.positionCount;
                    positionCount += 1;
                    _lineRenderer.positionCount = positionCount;
                    _lineRenderer.SetPosition(positionCount - 1, _hit.point);
                    remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                    _ray = new Ray2D(_hit.point - _ray.direction * 0.01f, Vector2.Reflect(_ray.direction, _hit.normal));
                }
                else
                {
                    var positionCount = _lineRenderer.positionCount;
                    positionCount += 1;
                    _lineRenderer.positionCount = positionCount;
                    _lineRenderer.SetPosition(positionCount - 1,
                        _ray.origin + _ray.direction * remainingLength);
                }
            }
        }

        private void OnEnable()
        {
            _bounceCount = BounceCount + 1;
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

        private void DestroyBullet()
        {
            _rigidbody2D.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            _iBulletManager.OnBulletDestroy();
        }
    }
}