using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float cloudSpeed = 0.5f;

    void Update()
    {
        transform.Translate(cloudSpeed * Time.deltaTime * -Vector3.right);
    }
}
