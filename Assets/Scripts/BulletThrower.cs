using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    private Transform _bulletTransform;

    public delegate void BulletAction();

    public event BulletAction OnCreateBullet;

    private void Awake()
    {
        _bulletTransform = GameObject.Find("BulletSpawnPosition").transform;
    }

    private void OnMouseUp()
    {
        if (!GameManager.SharedInstance.IsLevelCompleted && GameManager.SharedInstance.BulletCount > 0)
        {
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        var bullet = ObjectPooler.SharedInstance.GetPooledObject();
        bullet.transform.position = _bulletTransform.position;
        bullet.transform.rotation = _bulletTransform.rotation;
        bullet.SetActive(true);

        OnCreateBullet?.Invoke();
    }
}