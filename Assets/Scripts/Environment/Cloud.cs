using Assets.Scripts.Environment;
using System.Collections;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private readonly float cloudSpeed = 0.5f;
    private ICloudManager cloudManager;

    private void Awake()
    {
        cloudManager = FindObjectOfType<CloudCreator>() as ICloudManager;
    }

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
        cloudManager.OnCloudInvisible();

        StartCoroutine(DestroyCloud());
    }

    private IEnumerator DestroyCloud()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
