using UnityEngine;

public class EnvCreator : MonoBehaviour
{
    private BoxCollider2D _collider;
    private float _spawnSize;

    private void Awake()
    {
        SetReferences();
    }
    
    private void SetReferences()
    {
        _collider = GetComponent<BoxCollider2D>();

        _spawnSize = _collider.bounds.size.x;
    }

    public void Create()
    {
        new GameObject("Props");
    }
    
    private void OnValidate()
    {
        SetReferences();
    }
}