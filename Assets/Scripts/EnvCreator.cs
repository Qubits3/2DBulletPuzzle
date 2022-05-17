#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Core;


#if UNITY_EDITOR
public class EnvCreator : MonoBehaviour
{
    private BoxCollider2D _collider;
    private GameObject _props;
    private float _spawnRegion;
    [SerializeField, Range(1, 10)] private int treeCount = 2;
    [SerializeField, Range(1, 20)] private int grassCount = 5;

    private void Awake()
    {
        SetReferences();
    }

    private void SetReferences()
    {
        _collider = GetComponent<BoxCollider2D>();

        _spawnRegion = _collider.bounds.size.x;
    }

    public void SpawnTrees()
    {
        CreateProps();

        SpawnPrefab("Prefabs/Environment/Trees/Tree-1", "Tree", -(_spawnRegion / 2), _spawnRegion / 2, _props, treeCount);
    }

    public void SpawnGrasses()
    {
        CreateProps();

        SpawnPrefab("Prefabs/Environment/Grasses/Grass-1", "Grass", -(_spawnRegion / 2), _spawnRegion / 2, _props, grassCount);
    }

    private void DeleteObjectsWithTag(string tag)
    {
        foreach (var o in GameObject.FindGameObjectsWithTag(tag))
        {
            DestroyImmediate(o);
        }
    }

    private GameObject SpawnPrefab(string path, string tag, float minPos, float maxPos)
    {
        DeleteObjectsWithTag(tag);

        var o = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path)) as GameObject;
        o.transform.position = new Vector3(Random.Range(minPos, maxPos), gameObject.transform.position.y);
        EditorUtility.SetDirty(o);

        return o;
    }

    private GameObject SpawnPrefab(string path, string tag, float minPos, float maxPos, GameObject _parent)
    {
        DeleteObjectsWithTag(tag);

        var o = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path), _parent.transform) as GameObject;
        o.transform.position = new Vector3(Random.Range(minPos, maxPos), gameObject.transform.position.y);
        EditorUtility.SetDirty(o);

        return o;
    }

    private GameObject[] SpawnPrefab(string path, string tag, float minPos, float maxPos, int spawnCount)
    {
        DeleteObjectsWithTag(tag);

        int i;
        for (i = 0; i < spawnCount; i++)
        {
            var o = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path)) as GameObject;
            o.transform.position = new Vector3(Random.Range(minPos, maxPos), gameObject.transform.position.y);
            EditorUtility.SetDirty(o);
        }

        return new GameObject[i];
    }

    private GameObject[] SpawnPrefab(string path, string tag, float minPos, float maxPos, GameObject _parent, int spawnCount)
    {
        DeleteObjectsWithTag(tag);

        int i;
        for (i = 0; i < spawnCount; i++)
        {
            var o = PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path), _parent.transform) as GameObject;
            o.transform.position = new Vector3(Random.Range(minPos, maxPos), gameObject.transform.position.y);
            EditorUtility.SetDirty(o);
        }

        return new GameObject[i];
    }

    private void CreateProps()
    {
        if (!GameObject.Find("Props"))
        {
            _props = CreateGameObject("Props");
        } else
        {
            _props = GameObject.Find("Props");
        }
    }

    private GameObject CreateGameObject(string _name)
    {
        return new GameObject(_name);
    }

    private void OnValidate()
    {
        SetReferences();
    }
}
#endif
