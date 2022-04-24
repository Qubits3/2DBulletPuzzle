using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArmRotator : MonoBehaviour
{
    private GameManager _gameManager;
    
    public float maxLength;
    private LineRenderer _lineRenderer;
    private Ray2D _ray;
    private RaycastHit2D _hit;
    private Vector3 _direction;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawBulletPrediction();
    }

    private void DrawBulletPrediction()
    {
        _ray = new Ray2D(transform.position, transform.right);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainingLength = maxLength;

        _hit = Physics2D.Raycast(_ray.origin, _ray.direction, remainingLength, layerMask);
        if (_hit.collider)
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hit.point);
        }
        else
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1,
                _ray.origin + _ray.direction * remainingLength);
        }
    }

    private void FixedUpdate()
    {
        if (!_gameManager.IsLevelCompleted)
        {
            RotateArm();
        }
    }

    private void RotateArm()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}