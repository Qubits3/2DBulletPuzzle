using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]private float bulletSpeed = 5.0f;
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }
}
