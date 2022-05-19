using Assets.Scripts.Environment;
using Environment;
using System;
using UnityEditor;
using UnityEngine;

public class CloudCreator : MonoBehaviour, ICloudManager
{
    [SerializeField] private PropReferences propReferences;

    [SerializeField, Range(0, 30)] private int cloudCount = 15;

    private BoxCollider2D _collider;
    private float _spawnRegion;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();

        _spawnRegion = _collider.bounds.size.y;

        SpawnCloudRandomly(count: cloudCount);
    }

    public void OnCloudInvisible()
    {
        SpawnCloud(count: 1);
    }

    private void SpawnCloud(int count)
    {
        SpawnPrefab(propReferences.GetRandomCloud, 0, _spawnRegion, count);
    }

    private void SpawnCloudRandomly(int count)
    {
        SpawnPrefabAtRandomPosition(propReferences.GetRandomCloud, 0, _spawnRegion, count);
    }

    private GameObject[] SpawnPrefabAtRandomPosition(Func<GameObject> randomPropFunction, float minPos, float maxPos, int spawnCount)
    {
        int i;
        for (i = 0; i < spawnCount; i++)
        {
            var o = PrefabUtility.InstantiatePrefab(randomPropFunction.Invoke()) as GameObject;
            float randomScale = UnityEngine.Random.Range(0.7f, 1.2f);
            o.transform.localScale = new Vector3(randomScale, randomScale);

            o.transform.position = new Vector3(UnityEngine.Random.Range(minPos, maxPos) / 5,
                UnityEngine.Random.Range(minPos, maxPos));
        }

        return new GameObject[i];
    }

    private GameObject[] SpawnPrefab(Func<GameObject> randomPropFunction, float minPos, float maxPos, int spawnCount)
    {
        int i;
        for (i = 0; i < spawnCount; i++)
        {
            var o = PrefabUtility.InstantiatePrefab(randomPropFunction.Invoke()) as GameObject;
            float randomScale = UnityEngine.Random.Range(0.7f, 1.2f);
            o.transform.localScale = new Vector3(randomScale, randomScale);

            o.transform.position = new Vector3(UnityEngine.Random.Range(transform.position.x - 2, transform.position.x + 2),
                UnityEngine.Random.Range(minPos, maxPos));
        }

        return new GameObject[i];
    }
}
