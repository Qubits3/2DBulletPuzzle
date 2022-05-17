#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;

namespace Environment
{
#if UNITY_EDITOR
    public class EnvCreator : MonoBehaviour
    {
        [SerializeField] private PropReferences propReferences;

        private BoxCollider2D _collider;
        private GameObject _props;
        private float _spawnRegion;
        [SerializeField, Range(0, 10), Space(10)] private int bushCount = 2;
        [SerializeField, Range(0, 20)] private int flowerCount = 5;
        [SerializeField, Range(0, 20)] private int grassCount = 10;
        [SerializeField, Range(0, 20)] private int rockCount = 5;
        [SerializeField, Range(0, 20)] private int treeCount = 2;

        private void Awake()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            _collider = GetComponent<BoxCollider2D>();

            _spawnRegion = _collider.bounds.size.x;
        }

        public void SpawnBushes()
        {
            CreateProps();

            SpawnPrefab(propReferences.GetRandomBush, "Bush", -(_spawnRegion / 2), _spawnRegion / 2, _props, bushCount);
        }

        public void SpawnFlower()
        {
            CreateProps();

            SpawnPrefab(propReferences.GetRandomFlower, "Flower", -(_spawnRegion / 2), _spawnRegion / 2, _props, flowerCount);
        }

        public void SpawnGrasses()
        {
            CreateProps();

            SpawnPrefab(propReferences.GetRandomGrass, "Grass", -(_spawnRegion / 2), _spawnRegion / 2, _props, grassCount);
        }

        public void SpawnRocks()
        {
            CreateProps();

            SpawnPrefab(propReferences.GetRandomRock, "Rock", -(_spawnRegion / 2), _spawnRegion / 2, _props, rockCount);
        }

        public void SpawnTrees()
        {
            CreateProps();

            SpawnPrefab(propReferences.GetRandomTree, "Tree", -(_spawnRegion / 2), _spawnRegion / 2, _props, treeCount);
        }

        private void DeleteObjectsWithTag(string tag)
        {
            foreach (var o in GameObject.FindGameObjectsWithTag(tag))
            {
                DestroyImmediate(o);
            }
        }

        private GameObject SpawnPrefab(Func<GameObject> randomPropFunction, string tag, float minPos, float maxPos)
        {
            DeleteObjectsWithTag(tag);

            var o = PrefabUtility.InstantiatePrefab(randomPropFunction.Invoke()) as GameObject;
            o.transform.position = new Vector3(UnityEngine.Random.Range(minPos, maxPos), gameObject.transform.position.y);
            EditorUtility.SetDirty(o);

            return o;
        }

        private GameObject SpawnPrefab(Func<GameObject> randomPropFunction, string tag, float minPos, float maxPos, GameObject _parent)
        {
            DeleteObjectsWithTag(tag);

            var o = PrefabUtility.InstantiatePrefab(randomPropFunction.Invoke(), _parent.transform) as GameObject;
            o.transform.position = new Vector3(UnityEngine.Random.Range(minPos, maxPos), gameObject.transform.position.y);
            EditorUtility.SetDirty(o);

            return o;
        }

        private GameObject[] SpawnPrefab(Func<GameObject> randomPropFunction, string tag, float minPos, float maxPos, GameObject _parent, int spawnCount)
        {
            DeleteObjectsWithTag(tag);

            int i;
            for (i = 0; i < spawnCount; i++)
            {
                var o = PrefabUtility.InstantiatePrefab(randomPropFunction.Invoke(), _parent.transform) as GameObject;
                o.transform.position = new Vector3(UnityEngine.Random.Range(minPos, maxPos), gameObject.transform.position.y);
                EditorUtility.SetDirty(o);
            }

            return new GameObject[i];
        }

        public void ClearScene(string[] tags)
        {
            foreach (var tag in tags)
            {
                DeleteObjectsWithTag(tag);
            }
        }

        private void CreateProps()
        {
            if (!GameObject.Find("Props"))
            {
                _props = CreateGameObject("Props");
            }
            else
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
}
#endif
