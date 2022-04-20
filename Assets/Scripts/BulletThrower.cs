using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    public Transform gun;

    public void CreateBullet()
    {
        Instantiate(bullet, bulletTransform.position, gun.rotation);
    }
    void OnMouseDown()
    {
        CreateBullet();
    }
}
