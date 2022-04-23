using System;
using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    private Transform _bulletTransform;
    private Transform _gun;
    
    public delegate void BulletAction();

    public BulletAction OnCreateBullet;

    private void Awake()
    {
        _bulletTransform = GameObject.Find("BulletSpawnPosition").transform;
        _gun = GameObject.Find("Gun").transform;
    }

    private void OnMouseUp()
    {
        CreateBullet();
    }
    private void CreateBullet()
    {
        var bullet = ObjectPooler.SharedInstance.GetPooledObject();
        bullet.transform.position = _bulletTransform.position;
        bullet.transform.rotation = _gun.rotation;
        bullet.SetActive(true);
        
        OnCreateBullet?.Invoke();
    }
}