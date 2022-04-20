using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;

    public void CreateBullet()
    {
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);
    }
    private void OnMouseDown()
    {
        CreateBullet();
    }
}
