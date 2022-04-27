using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArmRotator : MonoBehaviour
{
    private GameManager _gameManager;
    
    private const float MaxLength = 100;
    private LineRenderer _lineRenderer;
    private Ray2D _ray;
    private RaycastHit2D _hit;
    private Vector3 _direction;
    private Camera _camera;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _lineRenderer = GetComponent<LineRenderer>();
        
        _camera = Camera.main;
    }
    
    private void FixedUpdate()
    {
        DrawBulletPrediction();
        
        if (_gameManager.CanShoot())
        {
            RotateArm();
        }
    }

    private void DrawBulletPrediction()
    {
        var transform1 = transform;
        var position = transform1.position;
        
        _ray = new Ray2D(position, transform1.right);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, position);

        _hit = Physics2D.Raycast(_ray.origin, _ray.direction, MaxLength, LayerMask.GetMask(""));
        if (_hit.collider)
        {
            var positionCount = _lineRenderer.positionCount;
            positionCount += 1;
            _lineRenderer.positionCount = positionCount;
            _lineRenderer.SetPosition(positionCount - 1, _hit.point);
        }
        else
        {
            var positionCount = _lineRenderer.positionCount;
            positionCount += 1;
            _lineRenderer.positionCount = positionCount;
            _lineRenderer.SetPosition(positionCount - 1,
                _ray.origin + _ray.direction * MaxLength);
        }
    }

    private void RotateArm()
    {
        Vector3 difference = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}