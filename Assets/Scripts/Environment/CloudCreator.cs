using Environment;
using System;
using UnityEditor;
using UnityEngine;

public class CloudCreator : MonoBehaviour
{
    [SerializeField] private PropReferences propReferences;

    [SerializeField, Range(0, 30)] private int cloudCount = 15;

    private BoxCollider2D _collider;
    private float _spawnRegion;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();

        _spawnRegion = _collider.bounds.size.y;

        SpawnPrefab(propReferences.GetRandomCloud, -_spawnRegion / 2, _spawnRegion / 2, cloudCount);
    }

    private GameObject[] SpawnPrefab(Func<GameObject> randomPropFunction, float minPos, float maxPos, int spawnCount)
    {
        int i;
        for (i = 0; i < spawnCount; i++)
        {
            var o = PrefabUtility.InstantiatePrefab(randomPropFunction.Invoke()) as GameObject;
            o.transform.position = new Vector3(UnityEngine.Random.Range(minPos, maxPos) / 2 , 
                UnityEngine.Random.Range(minPos, maxPos));
        }

        return new GameObject[i];
    }
}
