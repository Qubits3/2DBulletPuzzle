using System;
using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    private Transform _bulletTransform;

    private void Awake()
    {
        _bulletTransform = GameObject.Find("BulletSpawnPosition").transform;
    }

    private void OnMouseUp()
    {
        CreateBullet();
    }
    private void CreateBullet()
    {
        var bullet = ObjectPooler.SharedInstance.GetPooledObject();
        bullet.transform.position = _bulletTransform.position;
        bullet.transform.rotation = _bulletTransform.rotation;
        bullet.SetActive(true);
    }
}