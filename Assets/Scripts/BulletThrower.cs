using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    public Transform gun;

    private void CreateBullet()
    {
        Instantiate(bullet, bulletTransform.position, gun.rotation);
    }

    private void OnMouseDown()
    {
        CreateBullet();
    }
}