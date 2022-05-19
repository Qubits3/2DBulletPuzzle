using Assets.Scripts.Environment;
using System.Collections;
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
        transform.Translate(cloudSpeed * Time.deltaTime / transform.localScale.x * -Vector3.right);
    }

    private void OnBecameInvisible()
    {
        if (!gameObject.scene.isLoaded) return;
        (FindObjectOfType<CloudCreator>() as ICloudManager).OnCloudInvisible();

        StartCoroutine(DestroyCloud());
    }

    private IEnumerator DestroyCloud()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
