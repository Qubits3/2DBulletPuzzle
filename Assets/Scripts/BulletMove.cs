using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private GameObject gun;
    private void Awake()
    {
        gun = GameObject.Find("Gun");
    }
    void Update()
    {
        transform.position = gun.transform.up * 5;
    }
}
