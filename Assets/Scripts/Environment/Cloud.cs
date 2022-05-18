using Assets.Scripts.Environment;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float cloudSpeed = 0.5f;

    void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        transform.Translate(cloudSpeed * Time.deltaTime * -Vector3.right);
    }

    private void OnBecameInvisible()
    {
        if (!gameObject.scene.isLoaded) return;
        (FindObjectOfType<CloudCreator>() as ICloudManager).OnCloudInvisible();
    }
}
